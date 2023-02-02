using System;

namespace BikeApis.Models.Authentication
{
	public class AuthResponse
	{
		public string Token { get; set; }
		public DateTime Expiration { get; set; }
	}
}
