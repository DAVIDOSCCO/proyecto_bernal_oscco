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
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Cliente> BuscarPorId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return null;
                //return await connection.GetAllAsync<Categoria>().Result.Where(c => c.Id.Equals(id)).First();
            }
        }
        public async Task<IEnumerable<Cliente>> Listar(string nombre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nombre", nombre);
                return await connection.QueryAsync<Cliente>("dbo.UspBuscarClientes", parameters,
                                                        commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public async Task<int> Eliminar(int id) //soft delete
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return await connection.ExecuteAsync("update Cliente " +
                                                      "set Estado = 0 " +
                                                      "where Id = @id", parameters,
                                                        commandType: System.Data.CommandType.Text);
            }
        }
    }
}
