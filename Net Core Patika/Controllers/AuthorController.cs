using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Patika.Application.AuthorOperations.Commands;
using Net_Core_Patika.Application.GenreOperations.Commands;
using Net_Core_Patika.Application.GenreOperations.Commands.CreateGenre;
using Net_Core_Patika.Application.GenreOperations.Commands.DeleteGenre;
using Net_Core_Patika.Application.GenreOperations.Commands.UpdateGenre;
using Net_Core_Patika.Application.GenreOperations.Queries.GetGenreDetail;
using Net_Core_Patika.Application.GenreOperations.Queries.GetGenres;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;

        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorDetail(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;
            GetAuthorDetailQueryValidator validations = new GetAuthorDetailQueryValidator();
            validations.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newauthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context);
            command.Model = newauthor;

            CreateAuthorCommandValidator validations = new CreateAuthorCommandValidator();
            validations.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateAuthorModel newgenre)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.Model = newgenre;
            command.AuthorId = id;

            UpdateAuthorCommandValidator validations = new UpdateAuthorCommandValidator();
            validations.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthor command = new DeleteAuthor(_context);
            command.Id = id;

            DeleteAuthorCommandValidator validations = new DeleteAuthorCommandValidator();
            validations.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteAll()
        {
            DeleteAllAuthor command = new DeleteAllAuthor(_context);

            command.Handle();
            return Ok();
        }
    }
}
