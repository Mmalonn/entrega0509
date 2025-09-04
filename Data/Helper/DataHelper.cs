using entrega_viernes_5_09.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_viernes_5_09.Data.Helper
{
    public class DataHelper
    {
        private static DataHelper _instance;
        private SqlConnection _connection;

        private DataHelper()
        {
            _connection = new SqlConnection(Properties.Resource1.CadenaConexion);
        }
        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }
        public SqlConnection GetConnection()
        {
            return _connection;
        }
        public DataTable ExecuteSPQuery(string sp)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
                dt = null;
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }

        public DataTable ExecuteSPQuery(string sp, List<Parametro>? param = null)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;
                if (param != null)
                {
                    foreach (Parametro p in param)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Valor);
                    }
                }
                dt.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
                dt = null;
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }
        public bool ExecuteSpDml(string sp, List<Parametro>? param = null)
        {
            bool result;
            try
            {
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (param != null)
                {
                    foreach (Parametro p in param)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Valor);
                    }
                }

                int affectedRows = cmd.ExecuteNonQuery();

                result = affectedRows > 0;
            }
            catch (SqlException ex)
            {
                result = false;
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public bool ExecuteBillTransaction(Bill bill)
        {
            _connection.Open();
            SqlTransaction transaction = _connection.BeginTransaction();
            var cmd = new SqlCommand("SP_INSERTAR_FACTURA", _connection, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cliente", bill.Cliente);
            cmd.Parameters.AddWithValue("@idForma", bill.Payment.Id);
            cmd.Parameters.AddWithValue("@estaActivo", bill.estaActivo);
            SqlParameter param = new SqlParameter("@nroFactura", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);
            int affectedRows = cmd.ExecuteNonQuery();
            if (affectedRows <= 0)
            {
                transaction.Rollback();
                return false;
            }
            else
            {
                int billId = (int)param.Value;
                foreach (DetailBill d in bill.Details)
                {
                    SqlCommand cmdDetail = new SqlCommand("SP_INSERTAR_DETALLE", _connection, transaction);
                    cmdDetail.CommandType = CommandType.StoredProcedure;
                    cmdDetail.Parameters.AddWithValue("@nroFactura", billId);
                    cmdDetail.Parameters.AddWithValue("@idArticulo", d.Articulo.Id);
                    cmdDetail.Parameters.AddWithValue("@cantidad", d.Cantidad);
                    cmdDetail.Parameters.AddWithValue("@estaActivo", d.estaActivo);
                    int affectedRowsDetalle = cmdDetail.ExecuteNonQuery();
                    if (affectedRowsDetalle <= 0)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
                transaction.Commit();
                return true;
            }
        }
       
    }
}
