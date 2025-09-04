using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_viernes_5_09.Domain
{
    public class DetailBill
    {
        public Article Articulo { get; set; }
        public int Cantidad {  get; set; }
        public double Subtotal()
        {
            return Cantidad * Articulo.precioUnitario;
        }
    }
}
