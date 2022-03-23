using App.Ventas.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Ventas.Repositories.Dapper
{
    public class ColorRepository:Repository<Color>,IColorRepository
    {
        public ColorRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Color> BuscarPorId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return await connection.QuerySingleAsync<Color>("dbo.uspBuscarColorPorId", parameters,
                                                        commandType: System.Data.CommandType.StoredProcedure);
                //return null;
                //return await connection.GetAllAsync<Producto>().Result.Where(c => c.Id.Equals(id)).First();
            }
        }
        public async Task<IEnumerable<Color>> Listar(string Color)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Color", Color);
                return await connection.QueryAsync<Color>("dbo.UspBuscarColor", parameters,
                                                        commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public async Task<int> Eliminar(int id) //soft delete
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return await connection.ExecuteAsync("update Color " +
                                                      "set Estado = 0 " +
                                                      "where id= @id", parameters,
                                                        commandType: System.Data.CommandType.Text);
            }
        }
    }
}
