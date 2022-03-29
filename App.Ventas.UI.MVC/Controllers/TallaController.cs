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

        public ActionResult Error()
        {
            throw new System.Exception("Prueba de error para validar Action Filter");
        }

        // GET: Talla
        [HttpGet]
        public async Task<ActionResult> Index()
        {


            return View(await _unit.Tallas.Listar());
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        public async Task<ActionResult> Create(Talla talla)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Tallas.Agregar(talla);
                if (resp > 0)
                    //  return RedirectToAction("Index");

                    return (new JsonResult
                    {
                        ContentType = "application/json",
                        Data = resp
                    });
                else
                    return PartialView("_Create", talla);
            }
            // _log.Info("No funcionó el binding de talla (ModelState = false)");
            return PartialView("_Create", talla);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return PartialView("_Edit", await _unit.Tallas.Obtener(id));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Talla talla)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Tallas.Modificar(talla);
                if (resp)
                    return (new JsonResult
                    {
                        ContentType = "application/json",
                        Data = talla
                    });
                else
                    return PartialView("_Edit", talla);
            }
            return PartialView("_Edit", talla);
        }
        [HttpGet]

        public async Task<ActionResult> Delete(int id)
        {
            return PartialView("_Delete", await _unit.Tallas.Obtener(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if ((await _unit.Tallas.Eliminar(id)) != 0)

                return (new JsonResult
                {
                    ContentType = "application/json",
                    Data = id
                });

            return PartialView("_Delete", _unit.Tallas.Obtener(id));
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return PartialView("_Details", await _unit.Tallas.Obtener(id));
        }

        public async Task<PartialViewResult> List()
        {
            return PartialView("_List", await _unit.Tallas.Listar());
        }
        [Route("ListByFilters/{tallaId}/{tallaName}")]
        public async Task<PartialViewResult> ListByFilters(string tallaId, string tallaName)
        {
            List<Talla> lstTallas = new List<Talla>();

            if (!tallaId.Equals("-"))
            {
                var talla = await _unit.Tallas.Obtener(int.Parse(tallaId));
                lstTallas.Add(talla);
            }
            else if (!tallaName.Equals("-"))
            {
                var resultado = await _unit.Tallas.Listar(tallaName);
                lstTallas = resultado.ToList();
            }
            else
            {
                var resultado = await _unit.Tallas.Listar();
                lstTallas = resultado.ToList();
            }

            return PartialView("_List", lstTallas);
        }
    }
}