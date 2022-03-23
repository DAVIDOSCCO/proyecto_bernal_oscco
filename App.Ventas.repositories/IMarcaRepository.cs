using App.Ventas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Repositories
{
    public interface IMarcaRepository : IRepository<Marca>
    {
        Task<Marca> BuscarPorId(int id);
        Task<IEnumerable<Marca>> Listar(string marca);
        Task<int> Eliminar(int id);

    }
}
