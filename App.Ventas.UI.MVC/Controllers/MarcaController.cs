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
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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
                //var resp = await _unit.Marcas.Agregar(marca);

                var httpClient = new HttpClient();

                var credential = new Dictionary<string, string>
                {
                    {"username","davidoscco@gmail.com" },
                    {"password","12345" },
                    {"grant_type", "password" }
                };

                var response = await httpClient.PostAsync("https://localhost:44337/token",
                               new FormUrlEncodedContent(credential));

                var tokenContent = response.Content.ReadAsStringAsync().Result;

                var tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenContent);

                /*  consumir el servicio */


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    tokenDictionary["access_token"]);

                var content = JsonConvert.SerializeObject(marca);

                var buffer = Encoding.UTF8.GetBytes(content);

                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                /*  llamar al servicio */

                response = await httpClient.PostAsync("https://localhost:44337/api/marca",byteContent);

                var result = response.Content.ReadAsStringAsync().Result;

                var contentResult = JsonConvert.DeserializeObject<Dictionary<string, int>>(result);

 /*               if (resp > 0) */
                if (contentResult["id"] > 0)
                    //  return RedirectToAction("Index");

                    return (new JsonResult
                    {
                        ContentType = "application/json",
                        Data = contentResult["id"]  /*resp */
                    });
                else
                    return PartialView("_Create", marca);
            }
            // _log.Info("No funcionó el binding de marca (ModelState = false)");
            return PartialView("_Create",marca);
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
                // var resp = await _unit.Marcas.Modificar(marca);

                var httpClient = new HttpClient();

                var credential = new Dictionary<string, string>
                {
                    {"username","davidoscco@gmail.com" },
                    {"password","12345" },
                    {"grant_type", "password" }
                };

                var response = await httpClient.PostAsync("https://localhost:44337/token",
                               new FormUrlEncodedContent(credential));

                var tokenContent = response.Content.ReadAsStringAsync().Result;

                var tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenContent);

                /*  consumir el servicio */


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                    tokenDictionary["access_token"]);

                var content = JsonConvert.SerializeObject(marca);
                var buffer = Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response2 = await httpClient.PutAsync("https://localhost:44337/api/marca", byteContent);
                var result = response2.Content.ReadAsStringAsync().Result;
                var contentResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                // if (resp)
                if (contentResult["status"].Equals("true"))
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
