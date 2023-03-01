using System;
namespace Net_Core_Patika.TokenOperations.Models
{
	//
	public class Token
	{
		public string AccessToken { get; set; }
		public DateTime Expiration { get; set; }
		public string RefreshToken { get; set; }
	}
}

