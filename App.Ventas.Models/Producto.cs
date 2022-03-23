using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Models
{
    public class Producto
    {
        public int id {get; set;}
        public String Modelo { get; set; }
        public String Marca { get; set; }
        public String Color { get; set; }
        public string Talla { get; set; }
        public Byte[] Imagen { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Estado { get; set; }
        
    }
}
