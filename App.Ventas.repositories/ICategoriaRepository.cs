using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Ventas.Models;

namespace App.Ventas.Repositories
{
    public interface ICategoriaRepository: IRepository<Categoria>
    {
        Task<Categoria> BuscarPorId(int id);
        Task<IEnumerable<Categoria>> Listar(string nombre);
        Task<int> Eliminar(int id);
    }
}
