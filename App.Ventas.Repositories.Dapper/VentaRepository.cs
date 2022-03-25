using App.Ventas.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Repositories.Dapper
{
    public class VentaRepository : Repository<Venta>, IVentaRepository
    {
        public VentaRepository(string connectionString): base(connectionString)
        {

        }
        public async Task<IEnumerable<Venta>> ListarResumen(DateTime fecha)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return null;
            }
        }
        public async Task<bool> AnularVenta(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return false;
            }
        }

    }
}
