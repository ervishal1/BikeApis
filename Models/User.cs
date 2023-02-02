using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeApis.Models
{
	public class User : IdentityUser
	{

		public string ImageUri { get; set; }
		[NotMapped]
		public IFormFile ImageFile { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.Now;

		public ICollection<Bike> Bikes { get; set; } = new HashSet<Bike>();

		public ICollection<BikeLikes> Likes { get; set; } = new HashSet<BikeLikes>();

		public ICollection<BikeComment> Comments { get; set; } = new HashSet<BikeComment>();
	}
}
