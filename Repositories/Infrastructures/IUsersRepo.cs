using BikeApis.Models;
using BikeApis.ViewModels;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace BikeApis.Repositories.Infrastructures
{
	public interface IUsersRepo : IGenricRepo<User>
	{
		public bool Add(UserCreateViewModel user);
		public IEnumerable<User> Find(Expression<Func<User, bool>> expression);
	}
}
