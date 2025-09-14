using TransaccionesLibros.entities;
using TransaccionesLibros.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {

        IService<Invoice, int> InvoiceServ;

        public InvoicesController()
        {
            InvoiceServ = new UnitOfWork();
        }

        //1: Ok(Operacion Exitosa)
        //2: BadRequest(Error con Factura)
        //3: BadRequest(Error con Detalles de la Factura)

        [HttpGet("Bring")]
        public IActionResult Get()
        {
            List<Invoice>? invoices = InvoiceServ.Get();
            if (invoices != null && invoices.Count > 0)
            {
                return Ok(invoices);
            }
            else
            {
                return NotFound("No Registered Invoices Found");
            }
        }

        [HttpPost("Add")]
        public IActionResult Post([FromBody] Invoice invoice)
        {
            int result = InvoiceServ.Post(invoice);
            switch (result)
            {
                case 1:
                    return Ok("Invoice Successfully Registered");
                case 2:
                    return BadRequest("Error when trying to add the invoice");
                case 3:
                    return BadRequest("The invoice details could not be added correctly. (Operation canceled)");
                default:
                    throw new BadHttpRequestException("Revisar Servicio");
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            int result = InvoiceServ.Delete(id);
            switch (result)
            {
                case 1:
                    return Ok("Invoice Successfully Deleted");
                case 2:
                    return BadRequest("Invoice Could Not Be Deleted");
                case 4:
                    return BadRequest("There is no invoice with that ID/number");
                default:
                    throw new BadHttpRequestException("Revisar Servicio");
            }
        }


    }
}
