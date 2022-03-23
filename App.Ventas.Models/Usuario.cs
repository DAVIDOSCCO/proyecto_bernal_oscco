using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int TipoUsuario { get; set; }
        public bool Estado { get; set; }
        public int Rol { get; set; }
        //[Computed] //DataAnnotation de Dapper que indica que es un campo que no debe mapearse con la tabla de BD
        //public string ReturnUrl { get; set; }
    }
}
