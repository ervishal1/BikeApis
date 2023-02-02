using BikeApis.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BikeApis.DataAccessLayer
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<Bike> Bikes { get; set; }
		public DbSet<BikeType> BikeTypes { get; set; }
		public DbSet<BikeLikes> BikeLikes { get; set; }
		public DbSet<BikeComment> BikeComments { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
	}
}
