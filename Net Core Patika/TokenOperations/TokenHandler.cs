using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Net_Core_Patika.Entity;
using Net_Core_Patika.TokenOperations.Models;

namespace Net_Core_Patika.TokenOperations
{
	//token işlmeleri yapacağımız yer.
	public class TokenHandler
	{
		public IConfiguration Configuration { get; set; }

		public TokenHandler(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		//user alıcaz token yapıcaz usera göre
		public Token CreateAccessToken(User user)
		{
			Token tokenModel = new Token();
			SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));

			//geleni security key ile hazır algoritma kullanarak bu yol ile şifrele dedik.
			SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			//15 dakikalık token yaratık.
			tokenModel.Expiration = DateTime.Now.AddMinutes(15);

			//token ayarlarımız bu dedik.
			JwtSecurityToken securityToken = new JwtSecurityToken(
				issuer: Configuration["Token:Issuer"],
				audience: Configuration["Token:Audience"],
				expires: tokenModel.Expiration,
				notBefore: DateTime.Now,        //Ne zaman kullanılmaya başlansın biz şimdi dedik 5 dakika sonrada diyebilirdik.
				signingCredentials: credentials
				);

			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

			//Token yaratılıyor
			tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
			tokenModel.RefreshToken = CreateRefreshToken();

			return tokenModel;
		}

		public string CreateRefreshToken()
		{
			return Guid.NewGuid().ToString();
		}
	}

	
}

