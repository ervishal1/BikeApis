namespace BikeApis.Repositories.Infrastructures
{
	public interface IUnitOfWork
	{
		public IUsersRepo Users { get; }
		public IBikeTypeRepo BikeTypes  { get; }
		public IBikesRepo Bikes  { get; }
		public IBikeLikesRepo BikeLikes  { get; }
		public ICommentRepo BikeComments  { get; }

		void Save();
	}
}
