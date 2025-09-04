using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_viernes_5_09.Domain
{
    public class Bill
    {
        public int nroFactura { get; set; }
        public DateTime fecha { get; set; }
        public Payment Payment { get; set; }
        public string Cliente { get; set; }
        public bool estaActivo { get; set; }
        public List<DetailBill> Details { get; set; }
        public Bill()
        {
            Details = new List<DetailBill>();
        }
        public void AddDetail(DetailBill detail)
        {
            Details.Add(detail);
        }

        public void RemoveDetail(int id)
        {
            Details.RemoveAt(id);
        }
        public decimal Total()
        {
            decimal total = 0;
            foreach (DetailBill detail in Details)
            {
                total += detail.Subtotal();
            }
            return total;
        }
    }
}
