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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Security.Claims;
using System.Threading;

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

            var lstMarcas = await _unit.Marcas.Listar2();
            //Opción 1:
            ViewData["ListaMarcas"] = lstMarcas;

            //Opción 2:
            ViewBag.lstMarcas = lstMarcas;

            _log.Info("Ejecución del Controlador Producto, Método Index -- OK");
             return View(await _unit.Productos.Listar());
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
           
            var ListaMarcas = await _unit.Marcas.Listar();
            ViewBag.ListaMarcas = ListaMarcas;

            var ListaColores = await _unit.Colores.Listar();
            ViewBag.ListaColores = ListaColores;

            var ListaTallas= await _unit.Tallas.Listar();
            ViewBag.ListaTallas = ListaTallas;


            //return View();
            return PartialView("_Create");
        }



        [HttpPost]
        public async Task<ActionResult> Create(Producto producto)
        {
            
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
                    return (new JsonResult
                    {
                        ContentType = "application/json",
                        Data = resp
                    });
                else
                    return PartialView("_Create", producto);
            }
            _log.Info("No funcionó el binding de producto (ModelState = false)");
            return PartialView("_Create", producto);

            
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
           // var result = await _unit.Productos.Obtener(id);
           
            var ListaMarcas = await _unit.Marcas.Listar();
            ViewBag.ListaMarcas = ListaMarcas;

            var ListaColores = await _unit.Colores.Listar();
            ViewBag.ListaColores = ListaColores;

            var ListaTallas = await _unit.Tallas.Listar();
            ViewBag.ListaTallas = ListaTallas;

            return PartialView("_Edit", await _unit.Productos.Obtener(id));
            //return View(result);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Producto producto)
        {
                        
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
                if (resp>0)
                    return (new JsonResult
                    {
                        ContentType = "application/json",
                        Data = producto
                    });
                else
                    return PartialView("_Edit", producto);
            }
            return PartialView("_Edit", producto);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            //return View(await _unit.Productos.Obtener(id));
            return PartialView("_Delete", await _unit.Productos.Obtener(id));
        }
        
        //[HttpPost]
        //[ActionName("Delete")]
        //public async Task<ActionResult> DeletePost(int id)
        //{
        //    //if ((await _unit.Productos.Eliminar(id)) != 0) 
        //    //    return RedirectToAction("Index");

        //    //return View(_unit.Productos.Obtener(id));
        //    if ((await _unit.Categorias.Eliminar(id)) != 0)
        //        return (new JsonResult
        //        {
        //            ContentType = "application/json",
        //            Data = id
        //        });

        //    return PartialView("_Delete", _unit.Productos.Obtener(id));
        //}
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            // return View(await _unit.Productos.Obtener(id));
            return PartialView("_Details", await _unit.Productos.Obtener(id));
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
            if (resp>0)
            {
                return PartialView("_List", await _unit.Productos.Listar());
            }
            else
            {
                _log.Info("No se realizo la Venta porque la Cantidad es mayor que el Stock");
                return PartialView("_Compra", await _unit.Productos.Obtener(producto.id));
            }
            
        }


        public async Task<PartialViewResult> List()
        {
            return PartialView("_List", await _unit.Productos.Listar());
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

        [HttpGet]
        // GET: Productoes/Details/5
        public async Task<ActionResult> Details_WS(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var httpClient = new HttpClient();
            // var credential = new Dictionary<string, string>
            //    {
            //        {"username","cesbernal74@gmail.com" },
            //        {"password","123" },
            //        {"grant_type", "password" }
            //    };

            //var response = await httpClient.PostAsync("https://localhost:44337/token",
            //               new FormUrlEncodedContent(credential));

            //var tokenContent = response.Content.ReadAsStringAsync().Result;

            //var tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenContent);

            /*  consumir el servicio */


            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            //    tokenDictionary["access_token"]);

            var identity = ((ClaimsIdentity)Thread.CurrentPrincipal.Identity);
            var token = identity.Claims.Where(x => x.Type == CustomClaims.token).FirstOrDefault();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);


            //var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "jZ8LMyyGs3HFX3cWrjnuq2Kyhq9oSrjQn0lanDs4I2H2Lzl3N6ddsl-mjzLZQlw2E5KQc4syjnjOTvS48M1UW4YFwHAk58he0ywbNV-CfnzFuGX_ODwIy5TmsQ5QIQts3mGKRO17BmAgVWhv7zo7eSMxU1eARzWpDhQ5nMY4rfAze_CA5FS9R45Ioy8mWZ_KXyTF4eYTK0_xrhtDBOm0Ahq9UHbsbdz9o3qYOD-cd69QY4SS1lAnQyudAcMUff9Z");
            var json = await httpClient.GetStringAsync("https://localhost:44337/api/Producto/" + id);
            var DetalleProducto = JsonConvert.DeserializeObject<Producto>(json);

            //Producto producto = db.Producto.Find(id);
            //if (producto == null)
            //{
            //    return HttpNotFound();
            //}
            return PartialView("_Details", DetalleProducto);
        }

        [HttpPost]
        public async Task<ActionResult> Edit_WS(Producto producto)
        {

            //HttpPostedFileBase FileBase = Request.Files[0];

            //if (FileBase.ContentLength == 0)
            //{
            //    //string  imagenActual = Request.Form["Imagen2"].ToString();
            //}

            //else

            //{
            //    WebImage image = new WebImage(FileBase.InputStream);
            //    producto.Imagen = image.GetBytes();
            //}

            if (ModelState.IsValid)
            {
                var httpClient = new HttpClient();
                //var credential = new Dictionary<string, string>
                //{
                //    {"username","cesbernal74@gmail.com" },
                //    {"password","123" },
                //    {"grant_type", "password" }
                //};

                //var response = await httpClient.PostAsync("https://localhost:44337/token",
                //               new FormUrlEncodedContent(credential));

                //var tokenContent = response.Content.ReadAsStringAsync().Result;
                //var tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenContent);

                ///*  consumir el servicio */

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                //    tokenDictionary["access_token"]);
                var identity = ((ClaimsIdentity)Thread.CurrentPrincipal.Identity);
                var token = identity.Claims.Where(x => x.Type == CustomClaims.token).FirstOrDefault();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);

                var content = JsonConvert.SerializeObject(producto);
                var buffer = Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await httpClient.PutAsync("https://localhost:44337/api/Producto", byteContent);
                var result = response.Content.ReadAsStringAsync().Result;
                var contentResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);


                if (contentResult["status"].Equals("true"))
                {
                    Console.WriteLine(producto.id);
                    Console.WriteLine(producto.Modelo);
                    return (new JsonResult
                    {
                        ContentType = "application/json",
                        Data = producto
                    });

                }
                else
                    return PartialView("_Edit", producto);
            }
            return PartialView("_Edit", producto);
        }


        [HttpPost]
        //[ActionName("Delete")]
        public async Task<ActionResult> Delete_WS(Producto producto)
        {
            var httpClient = new HttpClient();
            //var credential = new Dictionary<string, string>
            //    {
            //        {"username","cesbernal74@gmail.com" },
            //        {"password","123" },
            //        {"grant_type", "password" }
            //    };

            //var response = await httpClient.PostAsync("https://localhost:44337/token",
            //               new FormUrlEncodedContent(credential));

            //var tokenContent = response.Content.ReadAsStringAsync().Result;
            //var tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenContent);

            ///*  consumir el servicio */

            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            //    tokenDictionary["access_token"]);

            var identity = ((ClaimsIdentity)Thread.CurrentPrincipal.Identity);
            var token = identity.Claims.Where(x => x.Type == CustomClaims.token).FirstOrDefault();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);

            var response = await httpClient.DeleteAsync("https://localhost:44337/api/Producto/" + producto.id);
            var result = response.Content.ReadAsStringAsync().Result;
            var contentResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);


            if (contentResult["delete"].Equals("true"))

                return (new JsonResult
                {
                    ContentType = "application/json",
                    Data = producto
                });

            return PartialView("_Delete", _unit.Productos.Obtener(producto.id));
        }

        [HttpPost]
        public async Task<ActionResult> Create_WS(Producto producto)
        {

            HttpPostedFileBase FileBase = Request.Files[0];  //Posicion 0
                                                             // Para recorrer varios archivos que vengan desde la pagina  HttpFileCollectionBase ColleccioBase=Request.File

            if (FileBase.ContentLength == 0)
            {

            }

            else

            {
                WebImage image = new WebImage(FileBase.InputStream);
                producto.Imagen = image.GetBytes();
            }


            if (ModelState.IsValid)
            {
                var httpClient = new HttpClient();
                //var credential = new Dictionary<string, string>
                //{
                //    {"username","cesbernal74@gmail.com" },
                //    {"password","123" },
                //    {"grant_type", "password" }
                //};

                //var response = await httpClient.PostAsync("https://localhost:44337/token",
                //               new FormUrlEncodedContent(credential));

                //var tokenContent = response.Content.ReadAsStringAsync().Result;
                //var tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenContent);

                ///*  consumir el servicio */

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                //    tokenDictionary["access_token"]);

                var identity = ((ClaimsIdentity)Thread.CurrentPrincipal.Identity);
                var token = identity.Claims.Where(x => x.Type == CustomClaims.token).FirstOrDefault();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);

                var content = JsonConvert.SerializeObject(producto);
                var buffer = Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await httpClient.PostAsync("https://localhost:44337/api/Producto", byteContent);
                var result = response.Content.ReadAsStringAsync().Result;
                var contentResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);


                if (contentResult["status"].Equals("true"))
                    return (new JsonResult
                    {
                        ContentType = "application/json",
                        Data = contentResult["id"]
                    });
                else
                    return PartialView("_Create", producto);
            }
            _log.Info("No funcionó el binding de producto (ModelState = false)");
            return PartialView("_Create", producto);


        }


    }
}