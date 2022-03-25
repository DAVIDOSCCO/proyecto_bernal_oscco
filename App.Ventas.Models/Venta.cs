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
        public DateTime FechaCreacion { get; set; }
        public decimal Total { get; set; }
        public int IdCliente { get; set; }

    }
}
