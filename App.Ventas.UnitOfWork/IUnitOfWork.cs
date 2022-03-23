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
       
        IProductoRepository Productos { get; }       
        IUsuarioRepository Usuarios { get; }

        IMarcaRepository Marcas { get; }
        IColorRepository Colores { get; }

        ITallaRepository Tallas { get; }
    }
}
