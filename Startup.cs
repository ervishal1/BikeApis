using BikeApis.DataAccessLayer;
using BikeApis.Extensions;
using BikeApis.Models;
using BikeApis.Models.Authentication;
using BikeApis.Repositories.Infrastructures;
using BikeApis.Repositories.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeApis
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
			//services.AddControllers();
			services.AddControllers().AddNewtonsoftJson(options =>
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
			);

			services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
			services.AddAuth(jwtSettings);

			services.AddDistributedMemoryCache();
			services.AddCors();
			services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
			services.AddDbContext<ApplicationDbContext>(op => op.UseSqlServer(Configuration.GetConnectionString("dbcon")));

			services.AddTransient(typeof(IGenricRepo<>), typeof(GenericRepo<>));
			services.AddTransient<IUsersRepo, UsersRepo>();
			services.AddTransient<IUnitOfWork, UnitOfWork>();
			services.AddTransient<IBikeTypeRepo, BikeTypeRepo>();
			services.AddTransient<IBikeLikesRepo, BikeLikesRepo>();
			services.AddTransient<ICommentRepo, CommentRepo>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(op => op.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			app.UseRouting();

			//app.UseAuthentication();
			//app.UseAuthorization();
			app.UseAuth();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
