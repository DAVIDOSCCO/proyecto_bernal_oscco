using App.Ventas.Models;
using App.Ventas.Repositories.Dapper;
using App.Ventas.UI.MVC.ViewModels;
using App.Ventas.UnitOfWork;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace App.Ventas.UI.MVC.Controllers
{
    [RoutePrefix("Usuario")]
    public class UsuarioController : BaseController
    {
        public UsuarioController(IUnitOfWork unit, ILog log) : base(unit, log)
        {
        }

        // GET: Categoria
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await _unit.Usuarios.Listar(""));
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        //Metodo Create: Proceso de creación de usuarios internos (Administrador, Vendedor)
        public async Task<ActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Usuarios.CrearUsuario(usuario);
                if (resp != null)
                    return RedirectToAction("Index");
                else
                    return View(usuario);
            }
            return View(usuario);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _unit.Usuarios.BuscarPorId(id));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Usuarios.ModificarUsuario(usuario);
                if (resp)
                    return RedirectToAction("Index");
                else
                    return View(usuario);
            }
            return View(usuario);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _unit.Usuarios.BuscarPorId(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if ((await _unit.Usuarios.Eliminar(id)) != 0) return RedirectToAction("Index");

            return View(_unit.Usuarios.Obtener(id));
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return View(await _unit.Usuarios.BuscarPorId(id));
        }

        //Proceso de creacion(sign up) y login(sign in) de usuarios
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string returnUrl)
        {
            return View(new UsuarioLoginViewModel { ReturnUrl = returnUrl });
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UsuarioLoginViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid) return View(usuarioViewModel);

            var usuarioValido = await _unit.Usuarios.ValidarUsuario(usuarioViewModel.Email, usuarioViewModel.Password);

            if(usuarioValido == null)
            {
                ModelState.AddModelError("Error", "Email o Password inválido");
                return View(usuarioViewModel);
            }

            var identity = new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.Email, usuarioValido.Email),
                new Claim(ClaimTypes.Role, usuarioValido.Rol.ToString()),
                new Claim(ClaimTypes.Name, $"{usuarioValido.Nombres} {usuarioValido.Apellidos}"),
                new Claim(ClaimTypes.NameIdentifier, usuarioValido.Id.ToString())
            }, "ApplicationCookie");

            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignIn(identity);

            return RedirectToLocal(usuarioViewModel.ReturnUrl);
        }
        public async Task<ActionResult> Logout()
        {
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignOut("ApplicationCookie");

            return RedirectToAction("Login", "Usuario");
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Signup()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        //Metodo Signup: Proceso de creación de usuarios externos
        public async Task<ActionResult> Signup(UsuarioRegisterViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    Nombres = usuarioViewModel.Nombres,
                    Apellidos = usuarioViewModel.Apellidos,
                    Email = usuarioViewModel.Email,
                    Login = usuarioViewModel.Login,
                    Password = usuarioViewModel.Password,
                    Rol = 3,
                    TipoUsuario = 0,
                    Estado = true
                };
                
                var resp = await _unit.Usuarios.CrearUsuario(usuario);
                if (resp != null)
                    return RedirectToAction("Index");
                else
                    return View(usuarioViewModel);
            }
            return View(usuarioViewModel);
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}