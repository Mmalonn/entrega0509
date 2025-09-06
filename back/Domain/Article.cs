using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_viernes_5_09.Domain
{
    public class Article
    {
        public  int Id { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario {  get; set; }
        public bool EstaActivo { get; set; }

        public override string ToString()
        {
            string activo;
            if (EstaActivo == true)
            {
                activo = "se encuentra activo";
            }
            else
            {
                activo = "No esta activo";
            }
                return Id + " - " + Nombre + " - " + activo + ". - $" + PrecioUnitario;

        }
    }
}
