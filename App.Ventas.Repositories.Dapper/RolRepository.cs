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
    public class RolRepository:Repository<Rol>, IRolRepository 
    {
        public RolRepository(string connectionString) : base(connectionString)
        {
        }
        public async Task<Rol> BuscarPorId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return null;
                //return await connection.GetAllAsync<Categoria>().Result.Where(c => c.Id.Equals(id)).First();
            }
        }
        public async Task<IEnumerable<Rol>> Listar(string nombre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nombre", nombre);
                return await connection.QueryAsync<Rol>("dbo.UspBuscarRol", parameters,
                                                        commandType: System.Data.CommandType.StoredProcedure);
            }
        }


        public async Task<int> Eliminar(int id) //soft delete
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return await connection.ExecuteAsync("DELETE  " +
                                                      "FROM ROL " +
                                                      "where Id = @id", parameters,
                                                        commandType: System.Data.CommandType.Text);
            }
        }
    }
}
