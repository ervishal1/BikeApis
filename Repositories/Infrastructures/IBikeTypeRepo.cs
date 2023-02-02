using BikeApis.Models;

namespace BikeApis.Repositories.Infrastructures
{
	public interface IBikeTypeRepo : IGenricRepo<BikeType>
	{
		public BikeType Update(BikeType model);
	}
}
