using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using System.Runtime.InteropServices;
using webapi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordatoriosController : ControllerBase
    {
        //DESARROLLAR AQUI EL METODO ASIGNADO EN CLASE
        //Los ejemplos de abajo son la base     para desarrollar el que te fue asignado en clase
        //Aqui se se invoca el DAO. Recuerda que debes desarrollar el metodo en el DAO  

        // GET: api/<RecordatoriosController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RecordatoriosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var recordatorio = new RecordatoriosDAO().GetOneById(id);
            return recordatorio == null ? NotFound() : Ok(recordatorio);
        }

        // POST api/<RecordatoriosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RecordatoriosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Recordatorios actRecordatorios)
        {

            if (actRecordatorios == null)
            {
                return BadRequest("No hay datos a actualizar.");
            }

            var existingRec = new RecordatoriosDAO().GetOneById(id);

            if (existingRec == null)
            {
                return NotFound("No se encontro el recordatorio.");
            }

            existingRec.fecha_recordatorio = actRecordatorios.fecha_recordatorio;

            ulong resultadoEdicion = new RecordatoriosDAO().Editar(existingRec);

            if (resultadoEdicion > 0)
            {
                return Ok(existingRec);
            }
            else
            {
                return StatusCode(500, "Error al actualizar.");
            }
        }


        // DELETE api/<RecordatoriosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
