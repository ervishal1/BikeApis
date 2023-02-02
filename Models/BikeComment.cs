using System;
using System.ComponentModel.DataAnnotations;

namespace BikeApis.Models
{
	public class BikeComment
	{
		[Key]
		public int Id { get; set; }

		public string UserId { get; set; }
		public User User { get; set; }

		public int BikeId { get; set; }
		public Bike Bike { get; set; }

		[Required,MinLength(3,ErrorMessage ="Minimum 3 Charecters is Required")]
		public string Comment { get; set; }

		[DataType("Date")]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

	}
}
