using System.ComponentModel.DataAnnotations;

namespace BikeApis.Models.Authentication
{
	public class AuthRequest
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
