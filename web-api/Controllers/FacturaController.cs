using entrega_viernes_5_09.Domain;
using entrega_viernes_5_09.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private BillService bServicio;

        public FacturaController()
        {
            bServicio = new BillService();
        }

        // GET: api/<FacturaController>
        [HttpGet("facturas")]
        public IActionResult Get()
        {

            return Ok(bServicio.GetBills());
        }

        // GET api/<FacturaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(bServicio.GetBill(id));
        }

        // POST api/<FacturaController>
        [HttpPost]
        public IActionResult Post([FromBody] Bill bill)
        {
            try
            {
                if (bill == null)
                {
                    return BadRequest("Se esperaba una factura");
                }
                if (bServicio.SaveBill(bill))
                    return Ok("Factura registrada con éxito!");
                else
                    return StatusCode(500, "No se pudo registrar la factura!");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno, intente nuevamente!");
            }
        }

        // PUT api/<FacturaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Bill bill)
        {
            try
            {
                if (bill == null)
                {
                    return BadRequest("Se esperaba una factura");
                }
                if (bServicio.UpdateBill(bill))
                    return Ok("Factura actualizada con éxito!");
                else
                    return StatusCode(500, "No se pudo registrar la factura!");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno, intente nuevamente!");
            }
        }

        // DELETE api/<FacturaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(bServicio.DeleteBill(id));
        }
    }
}
