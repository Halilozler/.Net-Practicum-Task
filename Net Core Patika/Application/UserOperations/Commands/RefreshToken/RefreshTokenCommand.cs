using System;
using AutoMapper;
using Net_Core_Patika.Application.UserOperations.Commands.CreateUser;
using Net_Core_Patika.DbOperations;
using Net_Core_Patika.TokenOperations;
using Net_Core_Patika.TokenOperations.Models;

namespace Net_Core_Patika.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }

        private readonly IBookStoreDbContext context;

        private readonly IConfiguration configuration;

        public RefreshTokenCommand(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        public Token Handle()
        {
            //Biz zaten tabloya refreshtokenı yazmıştık
            var user = context.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            if (user is not null)
            {
                //token günceleyip tekrar üstüne yazıyoruz.
                TokenHandler handler = new TokenHandler(configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);   //tekrar süreyi veriyoruz.

                context.SaveChanges();

                return token;
            }
            else
            {
                throw new InvalidOperationException("Valid bir refresh token bulunamadı!");
            }
        }
    }
}

