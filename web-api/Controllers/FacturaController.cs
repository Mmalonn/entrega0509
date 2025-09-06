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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FacturaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FacturaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FacturaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
