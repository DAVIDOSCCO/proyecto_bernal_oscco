using App.Ventas.Models;
using App.Ventas.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Ventas.Repositories.Dapper;
using System.Threading.Tasks;
using log4net;

namespace App.Ventas.UI.MVC.Controllers
{
    [RoutePrefix("Roles")]
    public class RolesController : BaseController
    {
        public RolesController(IUnitOfWork unit, ILog log) : base(unit, log)
        {
        }

        [HttpGet]
        // GET: Roles
        public async Task<ActionResult> Index()
        {
            return View(await _unit.Roles.Listar()); ;
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Rol rol)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Roles.Agregar(rol);
                if (resp > 0)
                    return RedirectToAction("Index");
                else
                    return View(rol);
            }
            return View(rol);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _unit.Roles.Obtener(id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Rol rol)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Roles.Modificar(rol);
                if (resp)
                    return RedirectToAction("Index");
                else
                    return View(rol);
            }
            return View(rol);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _unit.Roles.Obtener(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if ((await _unit.Roles.Eliminar(id)) != 0) return RedirectToAction("Index");

            return View(_unit.Roles.Obtener(id));
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return View(await _unit.Roles.Obtener(id));
        }
    }
}