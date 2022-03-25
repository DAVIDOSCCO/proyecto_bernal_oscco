using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Models
{
    public class TipoCategoria
    {
        /*Forma tradicional trabajando con atributos y getters y setters*/
        //Atributos o campos
        //private int id; 
        
        //Métodos accesores (getters y setters)
        /*
        public int getId()
        {
            return id;
        }
        public void setId(int id)
        {
            this.id = id;
        }
        */

        /*Trabajando con Propiedades*/
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }
}
