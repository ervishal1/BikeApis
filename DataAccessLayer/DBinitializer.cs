using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;
using BikeApis.Models;

namespace BikeApis.DataAccessLayer
{
	public class DBinitializer
	{
		public static async Task InitializeAsync(IServiceProvider serviceProvider, UserManager<User> _userManager)
		{
			var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			string[] roleNames = { "SuperAdmin", "CustomerCare" };
			IdentityResult result;
			foreach (var roleName in roleNames)
			{
				var roleExists = await RoleManager.RoleExistsAsync(roleName);
				if (!roleExists)
				{
					result = await RoleManager.CreateAsync(new IdentityRole(roleName));
				}
			}

			string Email = "superadmin@gmail.com";
			string Password = "Super@123";
			if (_userManager.FindByEmailAsync(Email).Result == null)
			{
				User user = new User();
				user.Email = Email;
				user.UserName = Email;
				user.EmailConfirmed = true;
				IdentityResult res = _userManager.CreateAsync(user, Password).Result;
				if (res.Succeeded)
				{
					_userManager.AddToRoleAsync(user, "SuperAdmin").Wait();
				}
			}
		}
	}
}
