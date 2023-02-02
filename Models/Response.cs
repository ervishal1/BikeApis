using System.Collections.Generic;

namespace BikeApis.Models
{
	public class Response
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }

		public User User { get; set; }
		public IList<User> Users{ get; set; }

		public BikeType BikeType { get; set; }
		public IList<BikeType> BikeTypes { get; set; }

		public Bike Bike { get; set; }
		public IList<Bike> Bikes { get; set; }
	}
}
