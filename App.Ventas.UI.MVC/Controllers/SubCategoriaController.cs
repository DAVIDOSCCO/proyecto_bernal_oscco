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
    [RoutePrefix("SubCategoria")]
    public class SubCategoriaController : BaseController
    {
        public SubCategoriaController(IUnitOfWork unit, ILog log) : base(unit, log)
        {
        }

        // GET: SubCategoria
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await _unit.SubCategorias.Listar());
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(SubCategoria subcategoria)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.SubCategorias.Agregar(subcategoria);
                if (resp > 0)
                    return RedirectToAction("Index");
                else
                    return View(subcategoria);
            }
            return View(subcategoria);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _unit.SubCategorias.Obtener(id));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(SubCategoria subcategoria)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.SubCategorias.Modificar(subcategoria);
                if (resp)
                    return RedirectToAction("Index");
                else
                    return View(subcategoria);
            }
            return View(subcategoria);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _unit.SubCategorias.Obtener(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if ((await _unit.SubCategorias.Eliminar(id)) != 0) return RedirectToAction("Index");

            return View(_unit.SubCategorias.Obtener(id));
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return View(await _unit.SubCategorias.Obtener(id));
        }
    }
    
}