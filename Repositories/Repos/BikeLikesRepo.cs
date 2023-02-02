using BikeApis.DataAccessLayer;
using BikeApis.Models;
using BikeApis.Repositories.Infrastructures;

namespace BikeApis.Repositories.Repos
{
	public class BikeLikesRepo : GenericRepo<BikeLikes>, IBikeLikesRepo
	{
		public BikeLikesRepo(ApplicationDbContext context) : base(context)
		{
		}
	}
}
