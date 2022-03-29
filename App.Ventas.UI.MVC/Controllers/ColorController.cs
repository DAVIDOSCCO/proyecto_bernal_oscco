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

        public ActionResult Error()
        {
            throw new System.Exception("Prueba de error para validar Action Filter");
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
            return PartialView("_Create");
        }
        [HttpPost]
        public async Task<ActionResult> Create(Models.Color color)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Colores.Agregar(color);
                if (resp > 0)
                    //  return RedirectToAction("Index");

                    return (new JsonResult
                    {
                        ContentType = "application/json",
                        Data = resp
                    });
                else
                    return PartialView("_Create", color);
            }
            // _log.Info("No funcionó el binding de color (ModelState = false)");
            return PartialView("_Create", color);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return PartialView("_Edit", await _unit.Colores.Obtener(id));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Models.Color color)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Colores.Modificar(color);
                if (resp)
                    return (new JsonResult
                    {
                        ContentType = "application/json",
                        Data = color
                    });
                else
                    return PartialView("_Edit", color);
            }
            return PartialView("_Edit", color);
        }
        [HttpGet]

        public async Task<ActionResult> Delete(int id)
        {
            return PartialView("_Delete", await _unit.Colores.Obtener(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if ((await _unit.Colores.Eliminar(id)) != 0)

                return (new JsonResult
                {
                    ContentType = "application/json",
                    Data = id
                });

            return PartialView("_Delete", _unit.Colores.Obtener(id));
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return PartialView("_Details", await _unit.Colores.Obtener(id));
        }

        public async Task<PartialViewResult> List()
        {
            return PartialView("_List", await _unit.Colores.Listar());
        }
        [Route("ListByFilters/{colorId}/{colorName}")]
        public async Task<PartialViewResult> ListByFilters(string colorId, string colorName)
        {
            List<Models.Color> lstColores = new List<Models.Color>();

            if (!colorId.Equals("-"))
            {
                var color = await _unit.Colores.Obtener(int.Parse(colorId));
                lstColores.Add(color);
            }
            else if (!colorName.Equals("-"))
            {
                var resultado = await _unit.Colores.Listar(colorName);
                lstColores = resultado.ToList();
            }
            else
            {
                var resultado = await _unit.Colores.Listar();
                lstColores = resultado.ToList();
            }

            return PartialView("_List", lstColores);
        }
    }
}