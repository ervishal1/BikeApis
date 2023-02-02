using BikeApis.Models;
using BikeApis.Models.Authentication;
using BikeApis.Repositories.Infrastructures;
using BikeApis.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BikeApis.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		protected readonly IUsersRepo _usersRepo;
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;
		private readonly JwtSettings _jwtSettings;

		public UserController(
			IUsersRepo usersRepo,
			SignInManager<User> signInManager,
			IOptionsSnapshot<JwtSettings> jwtSettings,
			UserManager<User> userManager)
		{
			_usersRepo = usersRepo;
			_signInManager = signInManager;
			_jwtSettings = jwtSettings.Value;
			_userManager = userManager;
		}

		[HttpGet]
		[Route("Test")]
		public IActionResult Test()
		{
			return StatusCode(StatusCodes.Status200OK, new { statusCode = 200, Message = "Tested Ok" });
		}

		[HttpPost]
		[Route("")]
		public IActionResult AddUser(UserCreateViewModel vm)
		{
			try
			{
				bool res = _usersRepo.Add(vm);
				if (res)
				{
					return StatusCode(StatusCodes.Status201Created, new Response { StatusCode = 201, Message = "User Created Successfully!!!" });
				}
				return StatusCode(StatusCodes.Status406NotAcceptable, new Response { StatusCode = 406, Message = "User Alrady Exist or Faild to Create !!!" });
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login(AuthRequest req)
		{
			try
			{
				var result = await _signInManager.PasswordSignInAsync(req.UserName, req.Password, false, false);
				if (result.Succeeded)
				{
					User user = _usersRepo.Find(x => x.Id == _userManager.GetUserId(User)).FirstOrDefault();
					var roles = await _userManager.GetRolesAsync(user);
					return Ok(GenerateJwt(user, roles));
					//return StatusCode(StatusCodes.Status200OK, new Response { Message = "User is Authenticated", StatusCode = 200 });
				}
				return StatusCode(StatusCodes.Status401Unauthorized, new Response { StatusCode = 401, Message = "User is Not Valid" });
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[Authorize(Roles = "SuperAdmin")]
		[HttpGet]
		[Route("")]
		public IActionResult GetAll()
		{
			try
			{
				var users = _usersRepo.GetAll().Select(x => new
				{
					x.Id,
					x.UserName,
					x.Email
				}).ToList();

				return StatusCode(StatusCodes.Status200OK, new { StatusCode = 200, Message = "Users Records", Users = users });
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[Authorize]
		[HttpPost]
		[Route("logout")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return StatusCode(StatusCodes.Status200OK, new
			{
				Message = "User Signed Out!"
			});
		}

		private string GenerateJwt(User user, IList<string> roles)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
			};

			var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
			claims.AddRange(roleClaims);

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

			var token = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Issuer,
				claims,
				expires: expires,
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
