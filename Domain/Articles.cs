using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_viernes_5_09.Domain
{
    public class Articles
    {
        public  int Id { get; set; }
        public string Nombre { get; set; }
        public double precioUnitario {  get; set; }
        public int EstaActivo { get; set; }

        public override string ToString()
        {
            string activo;
            if (EstaActivo == 1)
            {
                activo = "se encuentra activo";
            }
            else
            {
                activo = "No esta activo";
            }
                return Id + " - " + Nombre + " - " + activo + ". - $" + precioUnitario;

        }
    }
}
