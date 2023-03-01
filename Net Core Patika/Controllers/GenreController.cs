using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;

        private readonly IMapper _mapper;

        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpGet("{id}")]
        public IActionResult GetGenreDetail(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = id;
            GetGenreDetailQueryValidator validations = new GetGenreDetailQueryValidator();
            validations.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newgenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = newgenre;

            CreateGenreCommandValidator validations = new CreateGenreCommandValidator();
            validations.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel newgenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.Model = newgenre;
            command.GenreId = id;

            UpdateGenreCommandValidator validations = new UpdateGenreCommandValidator();
            validations.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = id;

            DeleteGenreCommandValidator validations = new DeleteGenreCommandValidator();
            validations.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}
