using App.Ventas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Repositories
{
    public interface IProductoRepository: IRepository<Producto>
    {
        Task<Producto> BuscarPorId(int id);
        Task<IEnumerable<Producto>> Listar(string nombre);
        Task<int> Eliminar(int id);
        Task<int> Actualizar(Producto producto);
    }
}
