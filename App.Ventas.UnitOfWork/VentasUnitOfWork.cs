using App.Ventas.Repositories;
using App.Ventas.Repositories.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.UnitOfWork
{
    public class VentasUnitOfWork: IUnitOfWork
    {
        public VentasUnitOfWork(string connectionString)
        {
          
            Productos = new ProductoRepository(connectionString);
           
           Usuarios = new UsuarioRepository(connectionString);

            Marcas = new MarcaRepository(connectionString);

            Colores = new ColorRepository(connectionString);

            Tallas = new TallaRepository(connectionString);

        }
      
        public IProductoRepository Productos
        {
            get;
            private set;
        }

        public IUsuarioRepository Usuarios
        {
            get;
            private set;
        }

        public IMarcaRepository Marcas
        {
            get;
            private set;
        }
        public IColorRepository Colores
        {
            get;
            private set;
        }


        public ITallaRepository Tallas
        {
            get;
            private set;
        }
    }


}
