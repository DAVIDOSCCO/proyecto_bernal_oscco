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
    public class MarcaRepository : Repository<Marca>, IMarcaRepository
    {
        public MarcaRepository(string connectionString) : base(connectionString)
        {
        }
        public async Task<Marca> BuscarPorId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return null;
                //return await connection.GetAllAsync<Categoria>().Result.Where(c => c.Id.Equals(id)).First();
            }
        }

        public async Task<int> Eliminar(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return await connection.ExecuteAsync("update Marca " +
                                                      "set Estado = 0 " +
                                                      "where Id = @id", parameters,
                                                        commandType: System.Data.CommandType.Text);
            }
        }

        public async Task<IEnumerable<Marca>> Listar(string nombre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Marca", nombre);
                return await connection.QueryAsync<Marca>("dbo.Usp_BuscarMarca", parameters,
                                                        commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Marca>> Listar2()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nombre", "");
                return await connection.QueryAsync<Marca>("select * from marca", parameters,
                                                        commandType: System.Data.CommandType.Text);
            }
        }
    }
}
