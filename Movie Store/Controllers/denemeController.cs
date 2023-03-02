using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Store.Entity;
using Movie_Store.Repository.Abstract;
using Movie_Store.Repository.Concrete;

namespace Movie_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DenemeController : ControllerBase
    {
        private IGenericRepository<Genre> _repository;

        public DenemeController(IGenericRepository<Genre> repositoru)
        {
            _repository = repositoru;
        }


    }
}
