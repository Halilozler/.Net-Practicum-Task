using System;
using AutoMapper;
using Net_Core_Patika.Application.UserOperations.Commands.CreateUser;
using Net_Core_Patika.DbOperations;
using Net_Core_Patika.TokenOperations;
using Net_Core_Patika.TokenOperations.Models;

namespace Net_Core_Patika.Application.UserOperations.Commands.CreateToken
{
	public class CreateTokenCommand
	{
        public CreateTokenModel Model { get; set; }

        private readonly IBookStoreDbContext context;
        private readonly IMapper mapper;

        //token yaratmak için IConfiguration ihtiyacımız var. çünkü token bilgilerini program.cs de tanımladık.
        private readonly IConfiguration configuration;

        public CreateTokenCommand(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public Token Handle()
        {
            var user = context.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if(user is not null)
            {
                //token yarat(TokenOperations içinde gerçekleştirdik)
                TokenHandler handler = new TokenHandler(configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                context.SaveChanges();

                return token;
            }
            else
            {
                throw new InvalidOperationException("Kullanıcı Adı - Şifre Hatalı!");
            }
        }
    }

    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

