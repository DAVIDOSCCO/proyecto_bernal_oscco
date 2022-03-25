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
            Categorias = new CategoriaRepository(connectionString);
            TipoCategorias = new TipoCategoriaRepository(connectionString);
            Clientes = new ClienteRepository(connectionString);
            Logs = new LogRepository(connectionString);
            Marcas = new MarcaRepository(connectionString);
            Productos = new ProductoRepository(connectionString);
            Roles = new RolRepository(connectionString);
            Usuarios = new UsuarioRepository(connectionString);
            SubCategorias = new SubCategoriaRepository(connectionString);
            Ventas = new VentaRepository(connectionString);
            
            Colores = new ColorRepository(connectionString);
            Tallas = new TallaRepository(connectionString);
        }
        public ICategoriaRepository Categorias
        {
            get;
            private set;
        }
        public ITipoCategoriaRepository TipoCategorias
        {
            get;
            private set;
        }
        public IClienteRepository Clientes
        {
            get;
            private set;
        }
        public ILogRepository Logs
        {
            get;
            private set;
        }
        public IMarcaRepository Marcas
        {
            get;
            private set;
        }
        public IProductoRepository Productos
        {
            get;
            private set;
        }
        public IRolRepository Roles
        {
            get;
            private set;
        }
        public IUsuarioRepository Usuarios
        {
            get;
            private set;
        }
        public ISubCategoriaRepository SubCategorias
        {
            get;
            private set;
        }
        public IVentaRepository Ventas
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
