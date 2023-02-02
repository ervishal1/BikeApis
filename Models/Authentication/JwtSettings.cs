namespace BikeApis.Models.Authentication
{
	public class JwtSettings
	{
		public string Issuer { get; set; }

		public string Secret { get; set; }

		public int ExpirationInDays { get; set; }
	}
}
