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
    public class LogRepository : Repository<Log>, ILogRepository
    {
        public LogRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Log> BuscarPorId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return null;
                //return await connection.GetAllAsync<Categoria>().Result.Where(c => c.Id.Equals(id)).First();
            }
        }
        public async Task<IEnumerable<Log>> Listar(string nombre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nombre", nombre);
                return await connection.QueryAsync<Log>("dbo.UspBuscarLogs", parameters,
                                                        commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public async Task<int> Eliminar(int id) //soft delete
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return await connection.ExecuteAsync("Delete from Log " +
                                                           "where Id = @id", parameters,
                                                        commandType: System.Data.CommandType.Text);
            }
        }
    }
}
