using App.Ventas.Models;
using App.Ventas.Repositories.Dapper;
using App.Ventas.UnitOfWork;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace App.Ventas.UI.MVC.Controllers
{
    [RoutePrefix("Cliente")]
    public class ClienteController : BaseController
    {
        public ClienteController(IUnitOfWork unit, ILog log) : base(unit, log)
        {
        }

        // GET: Cliente
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await _unit.Clientes.Listar());
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Clientes.Agregar(cliente);
                if (resp > 0)
                    return RedirectToAction("Index");
                else
                    return View(cliente);
            }
            return View(cliente);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _unit.Clientes.Obtener(id));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Clientes.Modificar(cliente);
                if (resp)
                    return RedirectToAction("Index");
                else
                    return View(cliente);
            }
            return View(cliente);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _unit.Clientes.Obtener(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if ((await _unit.Clientes.Eliminar(id)) != 0) return RedirectToAction("Index");

            return View(_unit.Clientes.Obtener(id));
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return View(await _unit.Clientes.Obtener(id));
        }
    }
}