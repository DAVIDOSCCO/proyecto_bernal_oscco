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
    [RoutePrefix("Venta")]
    public class VentaController : BaseController
    {
        public VentaController(IUnitOfWork unit, ILog log) : base(unit, log)
        {
        }

        // GET: Venta
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await _unit.Ventas.Listar());
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Venta venta)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Ventas.Agregar(venta);
                if (resp > 0)
                    return RedirectToAction("Index");
                else
                    return View(venta);
            }
            return View(venta);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _unit.Ventas.Obtener(id));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Venta venta)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Ventas.Modificar(venta);
                if (resp)
                    return RedirectToAction("Index");
                else
                    return View(venta);
            }
            return View(venta);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _unit.Ventas.Obtener(id));
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return View(await _unit.Ventas.Obtener(id));
        }
    }
}