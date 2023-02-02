using BikeApis.DataAccessLayer;
using BikeApis.Models;
using BikeApis.Repositories.Infrastructures;
using Microsoft.AspNetCore.Identity;

namespace BikeApis.Repositories.Repos
{
	public class UnitOfWork : IUnitOfWork
	{
		public IUsersRepo Users { get; protected set; }
		public IBikeTypeRepo BikeTypes { get; protected set; }
		public IBikesRepo Bikes { get; protected set; }
		public IBikeLikesRepo BikeLikes { get; protected set; }
		public ICommentRepo BikeComments { get; protected set; }

		public UnitOfWork(ApplicationDbContext context,UserManager<User> userManager)
		{
			this.context = context;
			Users = new UsersRepo(userManager,context);
			BikeTypes = new BikeTypeRepo(context);
			Bikes = new BikesRepo(context);
			BikeLikes = new BikeLikesRepo(context);
			BikeComments = new CommentRepo(context);
		}

		private readonly ApplicationDbContext context;

		public void Save()
		{
			context.SaveChanges();
		}
	}
}