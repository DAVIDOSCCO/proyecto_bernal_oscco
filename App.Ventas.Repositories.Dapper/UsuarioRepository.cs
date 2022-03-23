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
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {


        public UsuarioRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Usuario> BuscarPorId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return await connection.QuerySingleAsync<Usuario>("dbo.uspBuscarUsuarioPorId", parameters,
                                                        commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public async Task<IEnumerable<Usuario>> Listar(string nombre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@nombre", nombre);
                return await connection.QueryAsync<Usuario>("dbo.UspBuscarUsuario", parameters,
                                                        commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public async Task<int> Eliminar(int id) //soft delete
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return await connection.ExecuteAsync("update Usuario " +
                                                      "set Estado = 0 " +
                                                      "where Id = @id", parameters,
                                                        commandType: System.Data.CommandType.Text);
            }
        }

        public async Task<Usuario> ValidarUsuario(string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@email", email);
                parameters.Add("@password", password);
                
                var usuario = await connection.QueryFirstOrDefaultAsync<Usuario>("dbo.uspValidarUsuario", parameters,
                                                        commandType: System.Data.CommandType.StoredProcedure);

                return usuario;
            }
        }

        public async Task<Usuario> CrearUsuario(Usuario usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string messageResult = "";
                var parameters = new DynamicParameters();
                parameters.Add("@Nombres", usuario.Nombres);
                parameters.Add("@Apellidos", usuario.Apellidos);
                parameters.Add("@Email", usuario.Email);
                parameters.Add("@Login", usuario.Login);
                parameters.Add("@Password", usuario.Password);
                parameters.Add("@TipoUsuario", usuario.TipoUsuario);
                parameters.Add("@Rol", usuario.Rol);
                parameters.Add("@OV_Message_Result", messageResult, System.Data.DbType.String,
                    System.Data.ParameterDirection.Output);

                var usuarioCreado = await connection.QuerySingleAsync<Usuario>("dbo.uspCrearUsuario",
                    parameters, commandType: System.Data.CommandType.StoredProcedure);

                messageResult = parameters.Get<string>("@OV_Message_Result");

                return usuarioCreado;
            }
        }
        public async Task<bool> ModificarUsuario(Usuario usuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string messageResult = "";
                var parameters = new DynamicParameters();
                parameters.Add("@Id", usuario.Id);
                parameters.Add("@Nombres", usuario.Nombres);
                parameters.Add("@Apellidos", usuario.Apellidos);
                parameters.Add("@Email", usuario.Email);
                parameters.Add("@Login", usuario.Login);
                parameters.Add("@Password", usuario.Password);
                parameters.Add("@TipoUsuario", usuario.TipoUsuario);
                parameters.Add("@Rol", usuario.Rol);
                parameters.Add("@Estado", usuario.Estado);
                parameters.Add("@OV_Message_Result", messageResult, System.Data.DbType.String,
                    System.Data.ParameterDirection.Output);

                int resultado = await connection.ExecuteAsync("dbo.uspModificarUsuario",
                    parameters, commandType: System.Data.CommandType.StoredProcedure);

                messageResult = parameters.Get<string>("@OV_Message_Result");

                if (resultado > 0)
                    return true;

                return false;
            }
        }
    }
}
