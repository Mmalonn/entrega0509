using entrega_viernes_5_09.Data.Helper;
using entrega_viernes_5_09.Data.Repositorys;
using entrega_viernes_5_09.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_viernes_5_09.Data.Implements
{
    public class PaymentRepository : IPaymentRepository
    {
        public bool Delete(int id)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro()
                {
                    Name = "@id",
                    Valor = id
                }
            };
            try
            {
                DataHelper.GetInstance().ExecuteSPQuery("SP_REGISTRAR_BAJA_FORMAPAGO", parametros);
                var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_FORMAPAGO_POR_CODIGO", parametros);
                if ((bool)dt.Rows[0]["estaActivo"] == false)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        public List<Payment> GetAll()
        {
            List<Payment> lst = new List<Payment>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_FORMASPAGO");
            foreach (DataRow row in dt.Rows)
            {
                Payment p = new Payment();
                p.Id = (int)row["id"];
                p.Nombre = (string)row["nombre"];
                p.EstaActivo = (bool)row["estaActivo"];
                lst.Add(p);
            }
            return lst;
        }

        public Payment? GetById(int id)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro()
                {
                    Name = "@id",
                    Valor = id
                }
            };
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_FORMAPAGO_POR_CODIGO", parametros);
            if (dt.Rows.Count > 0)
            {
                Payment p = new Payment()
                {
                    Id = (int)dt.Rows[0]["id"],
                    Nombre = (string)dt.Rows[0]["nombre"],
                    EstaActivo = (bool)dt.Rows[0]["estaActivo"]
                };
                return p;
            }
            return null;
        }

        public bool Save(Payment payment)
        {
            bool exito;
            List<Parametro> param = new List<Parametro>()
            {
                new Parametro()
                {
                    Name = "@id",
                    Valor = payment.Id
                },
                new Parametro()
                {
                    Name = "@nombre",
                    Valor = payment.Nombre
                },
                new Parametro()
                {
                    Name = "@estaActivo",
                    Valor = payment.EstaActivo
                }
            };
            exito = DataHelper.GetInstance().ExecuteSpDml("SP_GUARDAR_FORMAPAGO", param);

            return exito;
        }
    }
}
