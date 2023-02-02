using BikeApis.DataAccessLayer;
using BikeApis.Models;
using BikeApis.Repositories.Infrastructures;

namespace BikeApis.Repositories.Repos
{
	public class BikeTypeRepo : GenericRepo<BikeType>, IBikeTypeRepo
	{
		protected readonly ApplicationDbContext _context;
		public BikeTypeRepo(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public BikeType Update(BikeType model)
		{
			var res = _context.BikeTypes.Update(model);
			return model;
		}
	}
}
