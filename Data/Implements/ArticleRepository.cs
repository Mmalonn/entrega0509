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
    public class ArticleRepository : IArticleRepository
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
                DataHelper.GetInstance().ExecuteSPQuery("SP_REGISTRAR_BAJA_ARTICULO", parametros);
                var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_ARTICULOS_POR_CODIGO", parametros);
                if ((bool)dt.Rows[0]["estaActivo"] == false)
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

        public List<Article> GetAll()
        {
            List<Article> lst = new List<Article>();
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_ARTICULOS");
            foreach (DataRow row in dt.Rows)
            {
                Article a = new Article();
                a.Id = (int)row["id"];
                a.Nombre = (string)row["nombre"];
                a.PrecioUnitario = (decimal)row["precioUnitario"];
                a.EstaActivo = (bool)row["estaActivo"];
                lst.Add(a);
            }
            return lst;
        }

        public Article? GetById(int id)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
                new Parametro()
                {
                    Name = "@id",
                    Valor = id
                }
            };
            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_ARTICULOS_POR_CODIGO", parametros);
            if (dt.Rows.Count > 0)
            {
                Article a = new Article()
                {
                    Id = (int)dt.Rows[0]["id"],
                    Nombre = (string)dt.Rows[0]["nombre"],
                    PrecioUnitario = (decimal)dt.Rows[0]["precioUnitario"],
                    EstaActivo = (bool)dt.Rows[0]["estaActivo"]
                };
                return a;
            }
            return null;
        }
        public bool Save(Article article)
        {
            bool exito;
            List<Parametro> param = new List<Parametro>()
            {
                new Parametro()
                {
                    Name = "@id",
                    Valor = article.Id
                },
                new Parametro()
                {
                    Name = "@nombre",
                    Valor = article.Nombre
                },
                new Parametro()
                {
                    Name = "@precioUnitario",
                    Valor = article.PrecioUnitario
                },
                new Parametro()
                {
                    Name = "@estaActivo",
                    Valor = article.EstaActivo
                }
            };
            exito = DataHelper.GetInstance().ExecuteSpDml("SP_GUARDAR_ARTICULO", param);

            return exito;
        }
    }
}
