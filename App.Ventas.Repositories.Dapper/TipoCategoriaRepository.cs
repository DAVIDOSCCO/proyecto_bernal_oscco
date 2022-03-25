
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
    public class TipoCategoriaRepository: Repository<TipoCategoria>, ITipoCategoriaRepository
    {
        public TipoCategoriaRepository(string connectionString): base(connectionString)
        {
        }
    }
}
