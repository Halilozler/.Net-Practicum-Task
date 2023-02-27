using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Patika.BookOperations.CreateBook;
using Net_Core_Patika.BookOperations.GetBookById;
using Net_Core_Patika.BookOperations.GetBooks;
using Net_Core_Patika.BookOperations.UpdateBook;
using Net_Core_Patika.DbOperations;
using static Net_Core_Patika.BookOperations.CreateBook.CreateBookCommand;
using static Net_Core_Patika.BookOperations.UpdateBook.UpdateBookCommand;

namespace Net_Core_Patika.Controllers
{
    //Veritabanı bağladık Service katmanı(BookOperations) oluşturduk onun üstünden işlemler gerçekleştirdik.
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            //GetBooksQuery altında yazdık.
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
            return book;
        }

        [HttpGet("query")]
        public IActionResult GetById([FromQuery] string id)
        {
            GetBookByIdCommand command = new GetBookByIdCommand(_context);
            try
            {
                var book = command.Handle(id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel postBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = postBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.Id = id;
                command.bookModel = updatedBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);

            if (book is null)
            {
                return BadRequest();
            }

            _context.Books.Remove(book);

            _context.SaveChanges();
            return Ok();
        }
    }
}
