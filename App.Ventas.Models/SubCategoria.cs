using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Models
{
    public class SubCategoria
    {
        public int Id { get; set; }
        public String Nombre {get; set;}
        public String Descripcion { get; set; }
        public bool Estado { get; set; }
        public int IdCategoria { get; set; }

    }
}
