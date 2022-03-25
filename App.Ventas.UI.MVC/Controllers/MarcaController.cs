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
    [RoutePrefix("Marca")]
    public class MarcaController : BaseController
    {
        public MarcaController(IUnitOfWork unit, ILog log) : base(unit, log)
        {
        }

        public ActionResult Error()
        {
            throw new System.Exception("Prueba de error para validar Action Filter");
        }

        // GET: Marca
        [HttpGet]
        public async Task<ActionResult> Index()
        {


            return View(await _unit.Marcas.Listar());
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return PartialView("_Create");
        }
        [HttpPost]
        public async Task<ActionResult> Create(Marca marca)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Marcas.Agregar(marca);
                if (resp > 0)
                    //  return RedirectToAction("Index");

                    return (new JsonResult
                    {
                        ContentType = "application/json",
                        Data = resp
                    });
                else
                    return PartialView("_Create", marca);
            }
            // _log.Info("No funcionó el binding de marca (ModelState = false)");
            return PartialView("_Create", marca);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return PartialView("_Edit", await _unit.Marcas.Obtener(id));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Marca marca)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Marcas.Modificar(marca);
                if (resp)
                    return (new JsonResult
                    {
                        ContentType = "application/json",
                        Data = marca
                    });
                else
                    return PartialView("_Edit", marca);
            }
            return PartialView("_Edit", marca);
        }
        [HttpGet]

        public async Task<ActionResult> Delete(int id)
        {
            return PartialView("_Delete", await _unit.Marcas.Obtener(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if ((await _unit.Marcas.Eliminar(id)) != 0)

                return (new JsonResult
                {
                    ContentType = "application/json",
                    Data = id
                });

            return PartialView("_Delete", _unit.Marcas.Obtener(id));
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return PartialView("_Details", await _unit.Marcas.Obtener(id));
        }

        public async Task<PartialViewResult> List()
        {
            return PartialView("_List", await _unit.Marcas.Listar());
        }
        [Route("ListByFilters/{marcaId}/{marcaName}")]
        public async Task<PartialViewResult> ListByFilters(string marcaId, string marcaName)
        {
            List<Marca> lstMarcas = new List<Marca>();

            if (!marcaId.Equals("-"))
            {
                var marca = await _unit.Marcas.Obtener(int.Parse(marcaId));
                lstMarcas.Add(marca);
            }
            else if (!marcaName.Equals("-"))
            {
                var resultado = await _unit.Marcas.Listar(marcaName);
                lstMarcas = resultado.ToList();
            }
            else
            {
                var resultado = await _unit.Marcas.Listar();
                lstMarcas = resultado.ToList();
            }

            return PartialView("_List", lstMarcas);
        }
    }
}
