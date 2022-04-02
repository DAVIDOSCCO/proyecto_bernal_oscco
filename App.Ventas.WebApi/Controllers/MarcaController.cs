using App.Ventas.Models;
using App.Ventas.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace App.Ventas.WebApi.Controllers
{
    [RoutePrefix("api/marca")]
    public class MarcaController : BaseController
    {
        public MarcaController(IUnitOfWork unit) : base(unit)
        {
        }

        // GET api/<controller>
        [HttpGet]
        [Route("list")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _unit.Marcas.Listar());
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(await _unit.Marcas.Obtener(id));
        }

        // POST api/<controller>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] Marca marca)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var idMarca = await _unit.Marcas.Agregar(marca);
            return Ok(new { id = idMarca });
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Put([FromBody] Marca marca)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!await _unit.Marcas.Modificar(marca)) return BadRequest("Id Incorrecto");
            return Ok(new { status = true });
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            if (await _unit.Marcas.Eliminar(id) <= 0) return BadRequest("No se eliminó el registro especificado");
            return Ok(new { delete = true });
        }
    }
}