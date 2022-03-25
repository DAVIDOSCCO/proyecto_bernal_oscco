using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Ventas.Models;

namespace App.Ventas.Repositories
{
    public interface ILogRepository:IRepository<Log>
    {
        Task<Log> BuscarPorId(int id);
        Task<IEnumerable<Log>> Listar(string nombre);
        Task<int> Eliminar(int id);
    }
}
