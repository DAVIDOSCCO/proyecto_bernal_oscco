using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Ventas.UI.MVC.ViewModels
{
    public class UsuarioRegisterViewModel
    {
        [Required]
        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Debe indicar el correo obligatoriamente")]
        [DataType(DataType.EmailAddress, ErrorMessage = "La estructura de email no es válida")]
        public string Email { get; set; }

        public string Login { get; set; }

        [Required(ErrorMessage = "Debe indicar una constraseña obligatoriamente")]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "El número de caracteres excede el límite permitido")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe confirmar la constraseña obligatoriamente")]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "El número de caracteres excede el límite permitido")]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación de contraseña no coinciden")]
        public string ConfirmPassword { get; set; }

        public int TipoUsuario { get; set; }

        public bool Estado { get; set; }

        public int Rol { get; set; }
    }
}