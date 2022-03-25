using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Models
{
    public class Marca
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Debe seleccionar una marca")]
        public String NMarca { get; set; }
        public String Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}
