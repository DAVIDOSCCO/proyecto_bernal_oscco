using App.Ventas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Repositories
{
    public interface IVentaRepository: IRepository<Venta>
    {
        Task<IEnumerable<Venta>> ListarResumen(DateTime fecha);
        Task<bool> AnularVenta(int id);
        Task<int> InsertarTx(Venta venta);

    }
}
