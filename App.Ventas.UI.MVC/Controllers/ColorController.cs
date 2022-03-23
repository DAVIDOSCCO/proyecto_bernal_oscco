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
    [RoutePrefix("Color")]
    public class ColorController : BaseController
    {
        public ColorController(IUnitOfWork unit, ILog log) : base(unit, log)
        {
        }

        // GET: Color
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            
            return View(await _unit.Colores.Listar());
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Models.Color color)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Colores.Agregar(color);
                if (resp > 0)
                    return RedirectToAction("Index");
                else
                    return View(color);
            }
            return View(color);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _unit.Colores.Obtener(id));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Models.Color color)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Colores.Modificar(color);
                if (resp)
                    return RedirectToAction("Index");
                else
                    return View(color);
            }
            return View(color);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _unit.Colores.Obtener(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if ((await _unit.Colores.Eliminar(id)) != 0) return RedirectToAction("Index");

            return View(_unit.Colores.Obtener(id));
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return View(await _unit.Colores.Obtener(id));
        }
    }
}