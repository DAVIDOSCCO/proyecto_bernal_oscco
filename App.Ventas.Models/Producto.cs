using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Models
{
    public class Producto
    {
        public int id {get; set;}
        public String Modelo { get; set; }
        
        [Required(ErrorMessage = "Debe seleccionar una Marca")]
        public String Marca { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un Color")]
        public String Color { get; set; }
        [Required(ErrorMessage = "Debe seleccionar una Talla")]
        public string Talla { get; set; }
        public Byte[] Imagen { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Estado { get; set; }
        
    }
}
