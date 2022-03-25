using App.Ventas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICategoriaRepository Categorias { get; }
        ITipoCategoriaRepository TipoCategorias { get; }
        IClienteRepository Clientes { get; }
        ILogRepository Logs { get; }
        IMarcaRepository Marcas { get; }
        IProductoRepository Productos { get; }       
        IRolRepository Roles { get; }
        IUsuarioRepository Usuarios { get; }
        ISubCategoriaRepository SubCategorias { get; }
        IVentaRepository Ventas { get; }

        IColorRepository Colores { get; }
        ITallaRepository Tallas { get; }
    }
}
