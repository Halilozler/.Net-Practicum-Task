using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.Controllers
{
    //Veritabanı bağlayıp Sadece Service katmanı oluşturmadan controller üstünde işlemler yaptık.
    [Route("api/[controller]")]
    [ApiController]
    public class SecondController : ControllerBase
    {
        //SQL işlemleri yapabilmek için bir Contexte ihityacımız var.
        //contexti zaten biz program.cs kısmında ilk uygulama oluştururken yaratıyor biz sadece erişim alıyoruz.
        private readonly BookStoreDbContext _context;
        public SecondController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
            return book;
        }

        [HttpGet("query")]
        public Book GetById([FromQuery] string id)
        {
            var book = _context.Books.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book postBook)
        {
            //gelen bookun bizde karşılığı var mı.
            var book = _context.Books.SingleOrDefault(x => x.Title == postBook.Title);

            if(book is not null)
            {
                return BadRequest();
            }
            _context.Books.Add(postBook);

            //Burada farklı olarak veritabanına biz verimizi kaydetmek istediğimizden dolayı SaveChane() metodu çalıştırmamız lazım.
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);

            if(book is null)
            {
                return BadRequest();
            }
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            _context.SaveChanges();
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
