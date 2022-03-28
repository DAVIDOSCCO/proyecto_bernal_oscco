using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public int idProducto { get; set; }
        public String Cliente { get; set; }
        public int Cantidad { get; set; }
        //public decimal Precio { get; set; }
        public DateTime  Fecha { get; set; }


    }
}
