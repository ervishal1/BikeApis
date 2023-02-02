using BikeApis.DataAccessLayer;
using BikeApis.Models;
using BikeApis.Repositories.Infrastructures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeApis.Repositories.Repos
{
	public class BikesRepo : GenericRepo<Bike>, IBikesRepo
	{
		protected readonly ApplicationDbContext _context;
		public BikesRepo(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public new IEnumerable<Bike> GetAll()
		{
			return _context.Bikes.Include(x => x.BikeLikes).Include(x => x.BikeType).Include(x => x.BikeComments);
		}

		public Bike Update(Bike model)
		{
			var bike = _context.Bikes.FirstOrDefault(x => x.Id == model.Id);
			if (bike == null)
				return null;
			else
			{
				bike.Name = model.Name;
				bike.Description = model.Description;
				bike.BikeTypeId= model.BikeTypeId;
				_context.Bikes.Update(bike);
				return model;
			}
		}
	}
}
