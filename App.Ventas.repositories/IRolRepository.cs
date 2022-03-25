using App.Ventas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Repositories
{
    public interface IRolRepository:IRepository<Rol>
    {
        Task<Rol> BuscarPorId(int id);
        Task<IEnumerable<Rol>> Listar(string nombre);
        Task<int> Eliminar(int id);
    }
}
