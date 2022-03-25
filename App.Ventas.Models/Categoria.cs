using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Models
{
    public class Categoria
    {
        //[Key] Annotation -> Data Annotation
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        [Required]
        public int IdTipoCategoria { get; set; }
    }
}
