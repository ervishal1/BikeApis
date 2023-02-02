using BikeApis.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikeApis.Repositories.Infrastructures
{
	public interface IBikesRepo : IGenricRepo<Bike>
	{
		public Bike Update(Bike model);
	}
}
