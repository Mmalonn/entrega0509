using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_viernes_5_09.Domain
{
    public class Payment
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public override string ToString()
        {
            return Id + " - " + Nombre;
        }
    }
}
