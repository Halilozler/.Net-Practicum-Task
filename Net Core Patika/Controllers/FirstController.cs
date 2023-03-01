using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Net_Core_Patika.Controllers
{
    //Veritabanı Bağlamadan direk olarak istekler yaptık.
    [Route("api/[controller]")]
    [ApiController]
    public class FirstController : ControllerBase
    {
        private static List<Book> _bookList = new List<Book>()
        {
            new Book { Id = 1, Title = "Lean Startup", GenreId = 1, //Personel
                PageCount = 200, PublishDate = new DateTime(2001, 06,12) },
            new Book { Id = 2, Title = "Herland", GenreId = 2, //Science
                PageCount = 250, PublishDate = new DateTime(2010, 06,12) },
            new Book { Id = 3, Title = "Dune", GenreId = 2, //Science
                PageCount = 550, PublishDate = new DateTime(2005, 06,12) }
        };

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _bookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _bookList.Where(x => x.Id == id).SingleOrDefault();
            return book;
        }

        [HttpGet("query")]
        public Book GetById([FromQuery] string id)
        {
            var book = _bookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book postBook)
        {
            //gelen bookun bizde karşılığı var mı.
            var book = _bookList.SingleOrDefault(x => x.Title == postBook.Title);

            if(book is not null)
            {
                return BadRequest();
            }
            _bookList.Add(postBook);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _bookList.SingleOrDefault(x => x.Id == id);

            if(book is null)
            {
                return BadRequest();
            }
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookList.SingleOrDefault(x => x.Id == id);

            if (book is null)
            {
                return BadRequest();
            }

            _bookList.Remove(book);
            return Ok();
        }
    }
}
