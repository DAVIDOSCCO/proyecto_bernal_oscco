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
    public class SubCategoriaRepository : Repository<SubCategoria>, ISubCategoriaRepository
    {
        public SubCategoriaRepository(String connectionString) : base(connectionString) { }

        public async Task<SubCategoria> BuscarPorId(int Id)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                return null;
            }
        }

        public async Task<IEnumerable<SubCategoria>> Listar(String nombre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {

                var parameters = new DynamicParameters();
                parameters.Add("@nombre", nombre);
                return await connection.QueryAsync<SubCategoria>("dbo.UspBuscarSubaCategorias", parameters,
                                                        commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> Eliminar(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return await connection.ExecuteAsync("update SubCategoria " +
                                                      "set Estado = 0 " +
                                                      "where Id = @id", parameters,
                                                        commandType: System.Data.CommandType.Text);
            }
        }

        public async Task<IEnumerable<SubCategoria>> ListarSubcategoriaXCategoria(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return await connection.QueryAsync<SubCategoria>("dbo.UspListarSubaCategoriaXCategoria", parameters,
                                                        commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}