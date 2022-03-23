using App.Ventas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Repositories
{
    public interface ITallaRepository : IRepository<Talla>
    {
        Task<Talla> BuscarPorId(int id);
        Task<IEnumerable<Talla>> Listar(string Talla);
        Task<int> Eliminar(int id);

    }
}
