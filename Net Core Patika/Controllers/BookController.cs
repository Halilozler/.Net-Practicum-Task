using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Patika.BookOperations.CreateBook;
using Net_Core_Patika.BookOperations.DeleteBook;
using Net_Core_Patika.BookOperations.GetBookById;
using Net_Core_Patika.BookOperations.GetBooks;
using Net_Core_Patika.BookOperations.UpdateBook;
using Net_Core_Patika.DbOperations;
using static Net_Core_Patika.BookOperations.CreateBook.CreateBookCommand;
using static Net_Core_Patika.BookOperations.UpdateBook.UpdateBookCommand;

namespace Net_Core_Patika.Controllers
{
    //Controllerı korumak istersek
    [Authorize]
    //Veritabanı bağladık Service katmanı(BookOperations) oluşturduk onun üstünden işlemler gerçekleştirdik.
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;

        //AutoMapperıa tanımlıyoruz. 
        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            //bu olaya Dependecy Injection denir.Direk bunlar dolar. Biz doldurmayız.
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            //GetBooksQuery altında yazdık.
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
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
            //try
            //{
                command.Id = id;
                //validator:
                GetBookByIdCommandValidator validations = new GetBookByIdCommandValidator();
                validations.ValidateAndThrow(command);

                var book = command.Handle();
                return Ok(book);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel postBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            //try
            //{
                command.Model = postBook;

                //validator yaptığımız yapıya yönlendirdik.
                CreateBookCommandValidator validations = new CreateBookCommandValidator();

                /* Konsola yazma :
                ValidationResult result = validations.Validate(command);

                if (!result.IsValid)
                {
                    foreach (var item in result.Errors)
                    {
                        Console.WriteLine("Özellik: " + item.PropertyName + "- Error Message: " + item.ErrorMessage);
                    }
                }
                else
                {
                    command.Handle();
                }
                */

                //biz burada zaten try-catch yapısı kullanıyoruz o yüzden direk olarak hataları throw ile exception'a döndürebiliriz
                //bunun içinde:
                validations.ValidateAndThrow(command);
                command.Handle();
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            //try
            //{
            command.Id = id;
                command.Model = updatedBook;

                //Validator:
                UpdateBookCommandValidator validations = new UpdateBookCommandValidator();
                validations.ValidateAndThrow(command);

                command.Handle();
            //}
            //catch (Exception ex)
            //{
            //   return BadRequest(ex.Message);
            //}

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            //try
            //{
            command.Id = id;
            DeleteBookCommandValidator validations = new DeleteBookCommandValidator();
            validations.ValidateAndThrow(command);

            command.Handle();
            //}
            //catch (Exception ex)
            //{
            //return BadRequest(ex.Message);
            //}
            return Ok();
        }
    }
}
