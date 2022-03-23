using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Ventas.UI.MVC.ViewModels
{
    public class UsuarioLoginViewModel
    {
        [Required(ErrorMessage = "Debe indicar el correo obligatoriamente")]
        [DataType(DataType.EmailAddress, ErrorMessage = "La estructura de email no es válida")]
        //[RegularExpression("",ErrorMessage = "La estructura de email no es válida")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe indicar una constraseña obligatoriamente")]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "El número de caracteres excede el límite permitido")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}