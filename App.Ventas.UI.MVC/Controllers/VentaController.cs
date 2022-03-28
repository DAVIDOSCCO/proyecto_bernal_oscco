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

        [HttpGet]
        public async Task<ActionResult> Index()
        {

            var lstMarcas = await _unit.Marcas.Listar2();
            //Opción 1:
            ViewData["ListaMarcas"] = lstMarcas;

            //Opción 2:
            ViewBag.lstMarcas = lstMarcas;

            _log.Info("Ejecución del Controlador Venta, Método Index -- OK");
            return View("Index",await _unit.Productos.Listar());
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

        [HttpGet]
        public async Task<ActionResult> Compra(int id)
        {
            // return View(await _unit.Productos.Obtener(id));
            return PartialView("_Compra", await _unit.Productos.Obtener(id));
        }


        [HttpPost]
        public async Task<ActionResult> Compra(Producto producto)
        {
            Venta venta = new Venta();
            venta.idProducto = producto.id;
            venta.Cliente = Request.Form["cliente"];
            venta.Fecha = DateTime.Now;
            venta.Cantidad = int.Parse(Request.Form["cantidad"]);
            //venta.Precio = Convert.ToDecimal(producto.Precio);

            //string userName = Request.Form["txtUserName"]; 
            //string password = Request.Form["txtPassword"];
            //if (userName.Equals("user", StringComparison.CurrentCultureIgnoreCase)
            //&& password.Equals("pass", StringComparison.CurrentCultureIgnoreCase))
            //{
            //    return Content("Login successful !");
            //}
            //else
            //{
            //    return Content("Login failed !");
            //}

            // return View(await _unit.Productos.Obtener(id));
            var resp = await _unit.Ventas.InsertarTx(venta);
            if (resp > 0)
            {
                return PartialView("_List", await _unit.Productos.Listar());
            }
            else
            {
                _log.Info("No se realizo la Venta porque la Cantidad es mayor que el Stock");
                return PartialView("_Compra", await _unit.Productos.Obtener(producto.id));
            }

        }

        [Route("ListByFilters/{productoId}/{productoName}")]
        public async Task<PartialViewResult> ListByFilters(string productoId, string productoName)
        {
            List<Producto> lstProductos = new List<Producto>();

            if (!productoId.Equals("-"))
            {
                var producto = await _unit.Productos.Obtener(int.Parse(productoId));
                lstProductos.Add(producto);
            }
            else if (!productoName.Equals("-"))
            {
                var resultado = await _unit.Productos.Listar(productoName);
                lstProductos = resultado.ToList();
            }
            else
            {
                var resultado = await _unit.Productos.Listar();
                lstProductos = resultado.ToList();
            }

            return PartialView("_List", lstProductos);
        }

    }
}