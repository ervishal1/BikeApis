using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeApis.Models
{
	public class Bike
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		[Required]
		public string Company { get; set; }

		[DataType("Date")]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		public int BikeTypeId { get; set; }
		public BikeType BikeType { get; set; }

		public string UserId { get; set; }
		public User User{ get; set; }

		
		public ICollection<BikeLikes> BikeLikes { get; set; } = new HashSet<BikeLikes>();

		public ICollection<BikeComment> BikeComments { get; set; } = new HashSet<BikeComment>();
	}
}
