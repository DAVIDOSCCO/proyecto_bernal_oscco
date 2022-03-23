using App.Ventas.Models;
using App.Ventas.Repositories.Dapper;
using App.Ventas.UnitOfWork;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using App.Ventas.UI.MVC.ViewModels;

namespace App.Ventas.UI.MVC.Controllers
{
    [RoutePrefix("Talla")]
    public class TallaController : BaseController
    {
        public TallaController(IUnitOfWork unit, ILog log) : base(unit, log)
        {
        }

        // GET: talla
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await _unit.Tallas.Listar());
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Talla talla)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Tallas.Agregar(talla);
                if (resp > 0)
                    return RedirectToAction("Index");
                else
                    return View(talla);
            }
            return View(talla);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _unit.Tallas.Obtener(id));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Talla talla)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Tallas.Modificar(talla);
                if (resp)
                    return RedirectToAction("Index");
                else
                    return View(talla);
            }
            return View(talla);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _unit.Tallas.Obtener(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if ((await _unit.Tallas.Eliminar(id)) != 0) return RedirectToAction("Index");

            return View(_unit.Tallas.Obtener(id));
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return View(await _unit.Tallas.Obtener(id));
        }
    }
}