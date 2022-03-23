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
    public  class ProductoRepository: Repository<Producto>, IProductoRepository
    {
        public ProductoRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Producto> BuscarPorId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return await connection.QuerySingleAsync<Producto>("dbo.uspBuscarProductoPorId", parameters,
                                                        commandType: System.Data.CommandType.StoredProcedure);
                //return null;
                //return await connection.GetAllAsync<Producto>().Result.Where(c => c.Id.Equals(id)).First();
            }
        }
        public async Task<IEnumerable<Producto>> Listar(string modelo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Modelo", modelo);
                return await connection.QueryAsync<Producto>("dbo.UspBuscarProducto", parameters,
                                                        commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public async Task<int> Eliminar(int id) //hard delete
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                return await connection.ExecuteAsync("delete from Producto " +
                                                      "where id= @id", parameters,
                                                        commandType: System.Data.CommandType.Text);
            }
        }

        public async Task<int> Actualizar(Producto producto) 
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", producto.id);
                parameters.Add("@Modelo", producto.Modelo);
                parameters.Add("@Marca", producto.Marca);
                parameters.Add("@Color", producto.Color);
                parameters.Add("@Talla", producto.Talla);
                parameters.Add("@Imagen", producto.Imagen);
                parameters.Add("@Stock", producto.Stock);
                parameters.Add("@Precio", producto.Precio);
                parameters.Add("@Estado", producto.Estado);


                string xSQL = "";
                if (producto.Imagen == null) 
                    xSQL="UPDATE Producto SET Modelo = @Modelo,[Marca] = @Marca,[Color] = @Color,[Talla] = @Talla,[Precio] = @Precio,[Stock] = @Stock,[Estado] = @Estado WHERE id = @id";
                else
                  xSQL = "UPDATE Producto SET Modelo = @Modelo,[Marca] = @Marca,[Color] = @Color,[Talla] = @Talla,[Imagen] = @Imagen,[Precio] = @Precio,[Stock] = @Stock,[Estado] = @Estado WHERE id = @id";

                return await connection.ExecuteAsync(xSQL, parameters,  commandType: System.Data.CommandType.Text);
            }
        }
    }
}
