using App.Ventas.Models;
using Dapper;
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

        public async Task<int> InsertarTx(Venta venta)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                
                parameters.Add("@idProducto", venta.idProducto);
                parameters.Add("@Cliente", venta.Cliente);
                parameters.Add("@Cantidad", venta.Cantidad);
                parameters.Add("@Fecha", venta.Fecha);
                //parameters.Add("@Precio", venta.Precio);


                return await connection.ExecuteAsync("dbo.Usp_InsertarVenta", parameters,
                                                         commandType: System.Data.CommandType.StoredProcedure);
                //return await connection.ExecuteAsync("insert into venta (idProducto,Cliente,Cantidad,Fecha) values (@idProducto,@Cliente,@Cantidad,@Fecha)"
                //,parameters,commandType: System.Data.CommandType.Text);
                //return false;
            }
        }
    }
}
