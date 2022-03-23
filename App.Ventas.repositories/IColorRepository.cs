using App.Ventas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Repositories
{
    public interface IColorRepository : IRepository<Color>
    {
        Task<Color> BuscarPorId(int id);
        Task<IEnumerable<Color>> Listar(string color);
        Task<int> Eliminar(int id);

    }
}
