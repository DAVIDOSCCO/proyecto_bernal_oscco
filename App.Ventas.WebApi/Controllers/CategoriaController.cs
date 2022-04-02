using App.Ventas.Models;
using App.Ventas.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace App.Ventas.WebApi.Controllers
{
    [RoutePrefix("api/category")]
    public class CategoriaController : BaseController
    {
        public CategoriaController(IUnitOfWork unit): base(unit)
        {
        }

        // GET api/<controller>
        [HttpGet]
        [Route("list")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _unit.Categorias.Listar());
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(await _unit.Categorias.Obtener(id));
        }

        // POST api/<controller>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var idCategoria = await _unit.Categorias.Agregar(categoria);
            return Ok(new { id = idCategoria });
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Put([FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (! await _unit.Categorias.Modificar(categoria)) return BadRequest("Id Incorrecto");
            return Ok(new { status = true });
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            if (await _unit.Categorias.Eliminar(id) <= 0) return BadRequest("No se eliminó el registro especificado");
            return Ok(new { delete = true });
        }
    }
}