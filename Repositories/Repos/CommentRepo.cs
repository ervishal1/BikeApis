using BikeApis.DataAccessLayer;
using BikeApis.Models;
using BikeApis.Repositories.Infrastructures;

namespace BikeApis.Repositories.Repos
{
	public class CommentRepo : GenericRepo<BikeComment>, ICommentRepo
	{
		public CommentRepo(ApplicationDbContext context) : base(context)
		{
		}
	}
}
