using App.Ventas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> BuscarPorId(int id);
        Task<IEnumerable<Cliente>> Listar(string nombre);
        Task<int> Eliminar(int id);
    }
}
