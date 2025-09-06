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
    public class BillRepository : IBillRepository
    {
        public bool Delete(int id)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro()
                {
                    Name = "@nroFactura",
                    Valor = id
                }
            };
            try
            {
                DataHelper.GetInstance().ExecuteSPQuery("SP_REGISTRAR_BAJA_FACTURA", parametros);
                var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_FACTURA_POR_ID", parametros);
                if ((bool)dt.Rows[0]["facturaActiva"] == false)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public List<Bill> GetAll()
        {
            List<Bill> lst = new List<Bill>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_FACTURAS");

            if (dt == null || dt.Rows.Count == 0)
            {
                return lst;
            }

            foreach (DataRow row in dt.Rows)
            {
                int nroFactura = (int)row["nroFactura"];
                Bill? oBill = null;
                foreach (Bill billExistente in lst)
                {
                    if (billExistente.nroFactura == nroFactura)
                    {
                        oBill = billExistente;
                        break;
                    }
                }
                if (oBill == null)
                {
                    oBill = new Bill();
                    oBill.nroFactura = nroFactura;
                    oBill.Cliente = (string)row["cliente"];
                    oBill.fecha = (DateTime)row["fecha"];
                    oBill.FacturaActiva = (bool)row["facturaActiva"];
                    oBill.Details = new List<DetailBill>();
                    lst.Add(oBill);
                }
                Article oArticle = new Article();
                oArticle.Id = (int)row["id"];
                oArticle.Nombre = (string)row["nombre"];
                oArticle.PrecioUnitario = (decimal)row["precioUnitario"];
                oArticle.EstaActivo = (bool)row["estaActivo"];
                DetailBill oDetail = new DetailBill();
                oDetail.Cantidad = (int)row["cantidad"];
                oDetail.Articulo = oArticle;
                oBill.Details.Add(oDetail);
            }
            return lst;
        }

        public Bill? GetById(int id)
        {
            List<Parametro> param = new List<Parametro>()
            {
                new Parametro() { Name = "@nroFactura", Valor = id }
            };
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_FACTURA_POR_ID", param);
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            Bill? bill = null;
            foreach (DataRow row in dt.Rows)
            {
                if (bill == null)
                {
                    bill = new Bill();
                    bill.nroFactura = (int)row["nroFactura"];
                    bill.Cliente = (string)row["cliente"];
                    bill.fecha = (DateTime)row["fecha"];
                    bill.FacturaActiva = (bool)row["facturaActiva"];
                    bill.Details = new List<DetailBill>();
                }
                Article article = new Article();
                article.Id = (int)row["id"];
                article.Nombre = (string)row["nombre"];
                article.PrecioUnitario = (decimal)row["precioUnitario"];
                article.EstaActivo = (bool)row["estaActivo"];
                DetailBill detail = new DetailBill();
                detail.Cantidad = (int)row["cantidad"];
                detail.Articulo = article;
                bill.Details.Add(detail);
            }
            return bill;
        }

        public bool Save(Bill bill)
        {
            return DataHelper.GetInstance().ExecuteBillTransaction(bill);
        }
    }
}
