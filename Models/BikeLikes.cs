using System.ComponentModel.DataAnnotations;

namespace BikeApis.Models
{
	public class BikeLikes
	{
		[Key]
		public int Id { get; set; }

		public string UserId { get; set; }
		public User User { get; set; }

		[Required]
		public int BikeId { get; set; }
		public Bike Bike { get; set; }
	}
}
