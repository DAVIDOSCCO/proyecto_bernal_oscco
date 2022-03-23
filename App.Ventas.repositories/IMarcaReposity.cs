using App.Ventas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Repositories
{
    public interface IMarcaReposity: IRepository<Marca>
    {
        Task<Marca> BuscarPorId(int id);
        Task<IEnumerable<Marca>> Listar(string nombre);
        Task<int> Eliminar(int id);
    }
}
