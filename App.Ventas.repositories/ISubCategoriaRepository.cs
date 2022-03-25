using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Ventas.Models;

namespace App.Ventas.Repositories
{
    public interface ISubCategoriaRepository:IRepository<SubCategoria>
    {
        
        Task<SubCategoria> BuscarPorId(int id);
        Task<IEnumerable<SubCategoria>> Listar(String Nombre);
        Task<int> Eliminar(int id);

        Task<IEnumerable<SubCategoria>> ListarSubcategoriaXCategoria(int id);
    }
}
