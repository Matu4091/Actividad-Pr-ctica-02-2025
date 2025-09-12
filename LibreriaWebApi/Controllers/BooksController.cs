using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransaccionesLibros.services;
using LibreriaConsola.entities;
using LibreriaConsola.services;

namespace LibreriaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        IService<Book, string> BooksService;
       
        public BooksController()
        {
            BooksService = new BookService();
        }

        //1: Ok (Operacion Exitosa)
        //2: BadRequest (Operacion Fallida)
        //3: BadRequest (Libro Existente)
        //4: NotFound (Libro no Encontrado)

        [HttpGet("Bring")]
        public IActionResult Get()
        {
            List<Book>? books = BooksService.Get();
            if (books != null && books.Count > 0)
            {
                return Ok(books);
            }
            else
            {
                return NotFound("No Registered Books Found");
            }
        }

        [HttpGet("Search")]
        public IActionResult GetById([FromQuery] string isbn)
        {
            Book? book = BooksService.GetById(isbn);
            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound("No Book Found with that ISBN");
            }
        }

        [HttpPost("Add")]
        public IActionResult Post([FromBody] Book book)
        {
            int result = BooksService.Post(book);
            switch (result)
            {
                case 1:
                    return Ok("Book Successfully Registered");
                case 2:
                    return BadRequest("The Book could not be registered");
                case 3:
                    return BadRequest("There is already a book with that ISBN");
                default:
                    throw new BadHttpRequestException("Revisar Servicio");
            }
        }

        [HttpPut("Edit")]
        public IActionResult Put([FromBody] Book book)
        {
            int result = BooksService.Put(book);
            switch (result)
            {
                case 1:
                    return Ok("Successfully Edited Book");
                case 2:
                    return BadRequest("The book could not be edited");
                case 4:
                    return NotFound("There is no book with that ISBN");
                default:
                    throw new BadHttpRequestException("Revisar Servicio");
            }
        }

        [HttpDelete("Unsubscribe")]
        public IActionResult Delete([FromQuery] string isbn)
        {
            int result = BooksService.Delete(isbn);
            switch (result)
            {
                case 1:
                    return Ok("Book Successfully Discharged");
                case 2:
                    return BadRequest("The Book could not be registered");
                case 4:
                    return NotFound("There is no book with that ISBN");
                default:
                    throw new BadHttpRequestException("Revisar Servicio");
            }
        }

    }
}
