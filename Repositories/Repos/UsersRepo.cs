using BikeApis.Models;
using BikeApis.Repositories.Infrastructures;
using BikeApis.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using BikeApis.DataAccessLayer;
using System.Linq;

namespace BikeApis.Repositories.Repos
{
	public class UsersRepo : GenericRepo<User> ,IUsersRepo
	{
		private readonly UserManager<User> _userManager;
		private readonly ApplicationDbContext _context;

		public UsersRepo(UserManager<User> userManager, ApplicationDbContext context):base(context)
		{
			_userManager = userManager;
			_context = context;
		}

		public bool Add(UserCreateViewModel model)
		{
			User user = new User();
			user.Email = model.Email;
			user.UserName = model.UserName;
			user.ImageUri = model.ImageUri;
			user.EmailConfirmed = true;
			IdentityResult res = _userManager.CreateAsync(user,model.Password).Result;
			if (res.Succeeded)
			{
				return true;
			}
			return false;
		}

		public IEnumerable<User> Find(Expression<Func<User, bool>> expression)
		{
			return _context.Users.Where(expression);
		}
	}
}
