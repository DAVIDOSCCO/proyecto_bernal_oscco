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
    [RoutePrefix("Producto")]
    public class ProductoController : BaseController
    {
        public ProductoController(IUnitOfWork unit, ILog log) : base(unit, log)
        {
        }

        // GET: Producto
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await _unit.Productos.Listar());
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            List<MarcaViewModel> lst = null;
            var dbMarca = await _unit.Marcas.Listar();
            lst =
                (from d in dbMarca
                 select new MarcaViewModel
                 {
                     id = d.id,
                     Marca = d.NMarca
                 }).ToList();

            List<SelectListItem> itemsMarca = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Marca.ToString(),
                    Value = d.Marca.ToString(),
                    Selected = false
                };
            });

            ViewBag.itemsMarca = itemsMarca;


            List<ColorViewModel> lst2 = null;
            var dbColor = await _unit.Colores.Listar();
            lst2 =
                (from d in dbColor
                 select new ColorViewModel
                 {
                     id = d.id,
                     Color = d.NColor
                 }).ToList();

            List<SelectListItem> itemsColor = lst2.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Color.ToString(),
                    Value = d.Color.ToString(),
                    Selected = false
                };
            });

            ViewBag.itemsColor = itemsColor;


            List<TallaViewModel> lst3 = null;
            var dbTalla = await _unit.Tallas.Listar();
            lst3 =
                (from d in dbTalla
                 select new TallaViewModel
                 {
                     id = d.id,
                     Talla = d.NTalla
                 }).ToList();

            List<SelectListItem> itemsTalla = lst3.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Talla.ToString(),
                    Value = d.Talla.ToString(),
                    Selected = false
                };
            });

            ViewBag.itemsTalla = itemsTalla;

            return View();
        }



        [HttpPost]
        public async Task<ActionResult> Create(Producto producto)
        {
            string strDDLValue = Request.Form["Marcas"].ToString();
            producto.Marca = strDDLValue;

            string strDDLValue2 = Request.Form["Colores"].ToString();
            producto.Color = strDDLValue2;

            string strDDLValue3 = Request.Form["Tallas"].ToString();
            producto.Talla = strDDLValue3;

            HttpPostedFileBase FileBase = Request.Files[0];  //Posicion 0
            // Para recorrer varios archivos que vengan desde la pagina  HttpFileCollectionBase ColleccioBase=Request.File

          if (FileBase.ContentLength==0)
            { 
            
            }

          else
          
            {
                WebImage image = new WebImage(FileBase.InputStream);
                producto.Imagen = image.GetBytes();
            }


            if (ModelState.IsValid)
            {
                var resp = await _unit.Productos.Agregar(producto);
                if (resp > 0)
                    return RedirectToAction("Index");
                else
                    return View(producto);
            }
            return View(producto);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _unit.Productos.Obtener(id);

            List<MarcaViewModel> lst = null;
            var dbMarca = await _unit.Marcas.Listar();
            lst =
                (from d in dbMarca
                 select new MarcaViewModel
                 {
                     id = d.id,
                     Marca = d.NMarca
                 }).ToList();

            List<SelectListItem> itemsMarca = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Marca.ToString(),
                    Value = d.Marca.ToString(),
                    Selected = false
                };
            });

            itemsMarca.Find(c => c.Value == result.Marca.ToUpper()).Selected = true;
            ViewBag.itemsMarca = itemsMarca;


            List<ColorViewModel> lst2 = null;
            var dbColor = await _unit.Colores.Listar();
            lst2 =
                (from d in dbColor
                 select new ColorViewModel
                 {
                     id = d.id,
                     Color = d.NColor
                 }).ToList();

            List<SelectListItem> itemsColor = lst2.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Color.ToString(),
                    Value = d.Color.ToString(),
                    Selected = false
                };
            });

            itemsColor.Find(c => c.Value == result.Color.ToUpper()).Selected = true;
            ViewBag.itemsColor = itemsColor;
            

            ViewBag.items = itemsColor;


            List<TallaViewModel> lst3 = null;
            var dbTalla = await _unit.Tallas.Listar();
            lst3 =
                (from d in dbTalla
                 select new TallaViewModel
                 {
                     id = d.id,
                     Talla = d.NTalla
                 }).ToList();

            List<SelectListItem> itemsTalla = lst3.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Talla.ToString(),
                    Value = d.Talla.ToString(),
                    Selected = false
                };
            });

            itemsTalla.Find(c => c.Value == result.Talla.ToUpper()).Selected = true;
            ViewBag.itemsTalla = itemsTalla;




            return View(result);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Producto producto)
        {
            string strDDLValue = Request.Form["Marcas"].ToString();
            producto.Marca = strDDLValue;

            string strDDLValue2 = Request.Form["Colores"].ToString();
            producto.Color = strDDLValue2;

            string strDDLValue3 = Request.Form["Tallas"].ToString();
            producto.Talla = strDDLValue3;

            
            HttpPostedFileBase FileBase = Request.Files[0]; 
            
            

            if (FileBase.ContentLength==0)
            {
                //string  imagenActual = Request.Form["Imagen2"].ToString();
            }

            else

            {
                WebImage image = new WebImage(FileBase.InputStream);
                producto.Imagen = image.GetBytes();
            }

            if (ModelState.IsValid)
            {
                var resp = await _unit.Productos.Actualizar(producto);
                if (resp==1)
                    return RedirectToAction("Index");
                else
                    return View(producto);
            }
            return View(producto);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _unit.Productos.Obtener(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int id)
        {
            if ((await _unit.Productos.Eliminar(id)) != 0) 
                return RedirectToAction("Index");

            return View(_unit.Productos.Obtener(id));
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return View(await _unit.Productos.Obtener(id));
        }

        [HttpGet]
        public async Task<ActionResult> getImage(int id)
        {
            var resp = await _unit.Productos.BuscarPorId(id);
            byte[] byteImage = resp.Imagen;

            //ImageConverter converter = new ImageConverter();
            //byte[] byteImage = (byte[])converter.ConvertTo(resp.Imagen, typeof(byte[]));

            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);

            image.Save(memoryStream, ImageFormat.Jpeg);
            //memoryStream.WriteTo(context.Response.OutputStream);
            memoryStream.Position = 0;

            return File (memoryStream,"image/jpg");

        }
    }
}