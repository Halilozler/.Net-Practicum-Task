using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Store.Entity;
using Movie_Store.Repository.Abstract;
using Movie_Store.Repository.Concrete;
using Store.Base.Response;
using Store.Entity.Dtos;
using Store.Service.Abstract;

namespace Movie_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DenemeController : CustomBaseController
    {
        private readonly IGenreService _service;

        public DenemeController(IGenreService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var persons = await _service.GetAllAsync();
            return CreateActionResultInstance(persons);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreDto dto)
        {
            var genre = await _service.InsertAsync(dto);

            return CreateActionResultInstance(genre);
        }
    }
}
