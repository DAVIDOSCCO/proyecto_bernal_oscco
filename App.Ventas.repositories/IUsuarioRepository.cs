using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Ventas.Models;

namespace App.Ventas.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> BuscarPorId(int id);
        Task<IEnumerable<Usuario>> Listar(string nombre);
        Task<int> Eliminar(int id);
        //Para el proceso de login de usuario:
        Task<Usuario> ValidarUsuario(string email, string password);
        Task<Usuario> CrearUsuario(Usuario usuario);
        Task<bool> ModificarUsuario(Usuario usuario);
    }
}
