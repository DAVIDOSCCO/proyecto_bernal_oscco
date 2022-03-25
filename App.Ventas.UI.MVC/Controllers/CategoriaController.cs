using App.Ventas.Models;
using App.Ventas.Repositories.Dapper;
using App.Ventas.UI.MVC.ActionFilters;
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
    [RoutePrefix("Categoria")]
    public class CategoriaController : BaseController
    {
        public CategoriaController(IUnitOfWork unit, ILog log):base(unit, log)
        {
        }

        public ActionResult Error()
        {
            throw new System.Exception("Prueba de error para validar Action Filter");
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var lstTipoCategoria =  await _unit.TipoCategorias.Listar();
            //Opción 1:
            ViewData["TipoCategorias"] = lstTipoCategoria;

            //Opción 2:
            ViewBag.TipoCategorias = lstTipoCategoria;

            _log.Info("Ejecución del Controlador Categoria, Método Index -- OK");
            return View(await _unit.Categorias.Listar());
             
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var tipoCategorias = await _unit.TipoCategorias.Listar();

            /*
             * Envío de datos para el select html mediante un IEnumerable<TipoCategoria> en el ViewBag
             * Sirve para la forma1 y forma2 que se indica en el html
             */
            ViewBag.ListaTipoCategorias = tipoCategorias;

            /*
             * Envío de datos para el select html mediante un SelectList en el ViewBag
             * Sirve para forma3 que se indica en el html
             */
            var lstItems = new SelectList(ViewBag.ListaTipoCategorias, "Id", "Nombre");
            ViewBag.ListaTipoCategorias2 = lstItems;

            /*
             * Envío de datos para el select html mediante un List<SelectListItem> en el ViewBag
             * Sirve para forma4 que se indica en el html
             */
            List<SelectListItem> lstItems2 = new List<SelectListItem>();
            foreach(var item in tipoCategorias)
            {
                lstItems2.Add(new SelectListItem
                {
                    Text = item.Nombre,
                    Value = item.Id.ToString()
                });
            }

            ViewBag.ListaTipoCategorias3 = lstItems;

            return PartialView("_Create");
        }
        [HttpPost]
        public async Task<ActionResult> Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Categorias.Agregar(categoria);
                if (resp > 0)
                    return (new JsonResult { 
                        ContentType = "application/json",
                        Data = resp
                    });
                else
                    return PartialView("_Create", categoria);
            }
            _log.Info("No funcionó el binding de categoria (ModelState = false)");
            return PartialView("_Create", categoria);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            //Forma1
            var tipoCategorias = await _unit.TipoCategorias.Listar();
            ViewBag.ListaTipoCategorias = tipoCategorias;

            //Forma2
            var lstItems = new SelectList(ViewBag.ListaTipoCategorias, "Id", "Nombre");
            ViewBag.ListaTipoCategorias2 = lstItems;

            ViewBag.Subcategorias = await _unit.SubCategorias.Listar();//_unit.SubCategorias.ListarSubcategoriaXCategoria(id);

            return PartialView("_Edit", await _unit.Categorias.Obtener(id));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var resp = await _unit.Categorias.Modificar(categoria);
                if (resp)
                    return (new JsonResult
                    {
                        ContentType = "application/json",
                        Data = categoria
                    });
                else
                    return PartialView("_Edit", categoria);
            }
            return PartialView("_Edit", categoria);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return PartialView("_Delete", await _unit.Categorias.Obtener(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if ((await _unit.Categorias.Eliminar(id)) != 0) 
                return (new JsonResult
                {
                    ContentType = "application/json",
                    Data = id
                });

            return PartialView("_Delete", _unit.Categorias.Obtener(id));
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return PartialView("_Details", await _unit.Categorias.Obtener(id));
        }
        public async Task<PartialViewResult> List()
        {
            return PartialView("_List", await _unit.Categorias.Listar());
        }
        [Route("ListByFilters/{categoriaId}/{categoriaName}")]
        public async Task<PartialViewResult> ListByFilters(string categoriaId, string categoriaName)
        {
            List<Categoria> lstCategorias = new List<Categoria>();

            if (!categoriaId.Equals("-"))
            {
                var categoria = await _unit.Categorias.Obtener(int.Parse(categoriaId));
                lstCategorias.Add(categoria);
            }
            else if(!categoriaName.Equals("-")){
                var resultado = await _unit.Categorias.Listar(categoriaName);
                lstCategorias = resultado.ToList();
            }
            else
            {
                var resultado = await _unit.Categorias.Listar();
                lstCategorias = resultado.ToList();
            }

            return PartialView("_List", lstCategorias);
        }
    }
}
