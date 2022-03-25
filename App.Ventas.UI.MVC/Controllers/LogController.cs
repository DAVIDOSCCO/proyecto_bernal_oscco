using App.Ventas.Models;
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
    public class LogController : BaseController
    {
        public LogController(IUnitOfWork unit, ILog log) : base(unit, log)
        {
        }

        // GET: Categoria
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await _unit.Logs.Listar());
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Log log)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Logs.Agregar(log);
                if (resp > 0)
                    return RedirectToAction("Index");
                else
                    return View(log);
            }
            return View(log);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _unit.Logs.Obtener(id));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Log log)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Logs.Modificar(log);
                if (resp)
                    return RedirectToAction("Index");
                else
                    return View(log);
            }
            return View(log);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _unit.Logs.Obtener(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if ((await _unit.Logs.Eliminar(id)) != 0) return RedirectToAction("Index");

            return View(_unit.Logs.Obtener(id));
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return View(await _unit.Logs.Obtener(id));
        }
    }
}