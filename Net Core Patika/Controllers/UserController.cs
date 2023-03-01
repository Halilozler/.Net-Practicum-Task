using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net_Core_Patika.Application.UserOperations.Commands.CreateToken;
using Net_Core_Patika.Application.UserOperations.Commands.CreateUser;
using Net_Core_Patika.Application.UserOperations.Commands.RefreshToken;
using Net_Core_Patika.DbOperations;
using Net_Core_Patika.TokenOperations.Models;

namespace Net_Core_Patika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;

        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration;

        public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            command.Model = newUser;
            command.Handle();

            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> Create([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
            command.Model = login;

            var Token = command.Handle();

            return Ok(Token);
        }

        //kullanıcıdan token alıcak ve geriye refreshtokenı geri döndürücek.
        [HttpGet("refreshtoken")]
        public ActionResult<Token> RefreshToken([FromBody] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
            command.RefreshToken = token;

            var result = command.Handle();

            return Ok(result);
        }
    }
}
