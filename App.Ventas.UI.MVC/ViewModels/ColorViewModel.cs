using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace App.Ventas.UI.MVC.ViewModels
{
    public class ColorViewModel
    {

        public int id { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un Color")]
        public string Color { get; set; }
        
    }
}