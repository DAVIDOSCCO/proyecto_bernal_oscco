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
    public class MarcaRepository:Repository<Marca>,IMarcaRepository
    {
        public MarcaRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Marca> BuscarPorId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return await connection.QuerySingleAsync<Marca>("dbo.uspBuscarMarcaPorId", parameters,
                                                        commandType: System.Data.CommandType.StoredProcedure);
                //return null;
                //return await connection.GetAllAsync<Producto>().Result.Where(c => c.Id.Equals(id)).First();
            }
        }
        public async Task<IEnumerable<Marca>> Listar(string marca)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Marca", marca);
                return await connection.QueryAsync<Marca>("dbo.UspBuscarMarca", parameters,
                                                        commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public async Task<int> Eliminar(int id) //soft delete
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return await connection.ExecuteAsync("update Marca " +
                                                      "set Estado = 0 " +
                                                      "where id= @id", parameters,
                                                        commandType: System.Data.CommandType.Text);
            }
        }
    }
}
