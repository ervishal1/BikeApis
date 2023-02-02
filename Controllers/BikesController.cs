using BikeApis.Models;
using BikeApis.Repositories.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BikeApis.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class BikesController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<User> _userManager;

		public BikesController(IUnitOfWork unitOfWork, UserManager<User> userManager)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
		}

		[HttpGet]
		[Route("")]
		public IActionResult GetAll()
		{
			try
			{
				var result = _unitOfWork.Bikes.GetAll().Select(x => new
				{
					x.Id,
					x.Name,
					x.Description,
					x.Company,
					x.BikeTypeId,
					Type = x.BikeType.Title,
					likes = x.BikeLikes.Count,
					Comments = x.BikeComments.Select(x => x.Comment)
				}).ToList();
				return StatusCode(StatusCodes.Status200OK,result);
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost]
		[Route("")]
		public  IActionResult Add(Bike model)
		{
			try
			{
				var userId = _userManager.GetUserId(User);
				model.UserId= userId;
				var res = _unitOfWork.Bikes.Create(model);
				_unitOfWork.Save();
				if (res)
				{
					return StatusCode(StatusCodes.Status201Created, new Response
					{
						StatusCode = 201,
						Message = "Bike Added!"
					});
				}
				return StatusCode(StatusCodes.Status203NonAuthoritative, new Response
				{
					StatusCode = 203,
					Message = "Bike Not Added Try Again!"
				});
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPut]
		[Route("{id}")]
		public IActionResult Update(int? id,Bike model)
		{
			try
			{
				var userId = _userManager.GetUserId(User);
				//model.UserId = userId;
				var IsExist = _unitOfWork.Bikes.Find(x => x.Id == id).FirstOrDefault();
				if(IsExist == null)
				{
					return StatusCode(StatusCodes.Status404NotFound, new Response
					{
						StatusCode = 404,
						Message = "Bike Not Found!",
						Bike = model
					});
				}

				if (IsExist.UserId != userId)
				{
					return StatusCode(StatusCodes.Status401Unauthorized, new Response
					{
						StatusCode = 401,
						Message = "Unauthorized User!",
						Bike = model
					});
				}

				var res = _unitOfWork.Bikes.Update(model);
				_unitOfWork.Save();
				if (res != null)
				{
					return StatusCode(StatusCodes.Status201Created, new Response
					{
						StatusCode = 200,
						Message = "Bike Updated!"
					});
				}
				return StatusCode(StatusCodes.Status203NonAuthoritative, new Response
				{
					StatusCode = 203,
					Message = "Bike Not Updated Try Again!"
				});
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpDelete]
		[Route("{id}")]
		public IActionResult Delete(int id)
		{
			try
			{
				var userId = _userManager.GetUserId(User);
				var result = _unitOfWork.Bikes.GetById(id);

				if (result != null)
				{
					if (result.UserId != userId)
					{
						return StatusCode(StatusCodes.Status401Unauthorized, new Response
						{
							StatusCode = 401,
							Message = "Unauthorized User!",
						});
					}

					_unitOfWork.Bikes.Delete(result);
					_unitOfWork.Save();
					return StatusCode(StatusCodes.Status200OK, new Response { StatusCode = 200, Message = "Bike Is Deleted!", Bike = result });
				}
				return StatusCode(StatusCodes.Status404NotFound, new Response { StatusCode = 404, Message = "Bike Not Found!" });
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet]
		[Route("{id}")]
		public IActionResult GetById(int id)
		{
			try
			{
				var result = _unitOfWork.Bikes.GetAll().Select(x => new
				{
					x.Id,
					x.Name,
					x.Description,
					x.Company,
					x.BikeTypeId,
					Type = x.BikeType.Title,
					likes = x.BikeLikes.Count,
					Comments = x.BikeComments.Select(x => x.Comment)
				}).FirstOrDefault(x => x.Id == id);

				if (result != null)
				{
					return StatusCode(StatusCodes.Status200OK, new { StatusCode = 200, Message = "Bike Found" , Bike = result } );
				}
				return StatusCode(StatusCodes.Status404NotFound, new Response { StatusCode = 404, Message = "Bike Not Found!" });
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet]
		[Route("byType/{type}")]
		public IActionResult GetByType(string type)
		{
			try
			{
				var bikes = _unitOfWork.Bikes.GetAll().Where(x => x.BikeType.Title.Contains(type)).Select(x => new
				{
					x.Id,
					x.Name,
					x.Description,
					x.Company,
					x.BikeTypeId,
					Type = x.BikeType.Title,
					likes = x.BikeLikes.Count,
					Comments = x.BikeComments.Select(x => x.Comment)
				}).ToList();
				if(bikes != null)
				{
					return StatusCode(StatusCodes.Status200OK, new { StatusCode = 200, Message = $"Bike Found by Type like {type}", Bikes = bikes });
				}
				return StatusCode(StatusCodes.Status404NotFound, new Response { StatusCode = 404, Message = "Bike Not Found!" });
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost]
		[Route("like/{id}")]
		public IActionResult AddLike(int id,BikeLikes like)
		{
			var BikeIsExist = _unitOfWork.Bikes.Find(x => x.Id == id).FirstOrDefault();

			if (id == 0)
			{
				return StatusCode(StatusCodes.Status206PartialContent, new Response { StatusCode = 206, Message = "BikeId is Required!" });
			}

			if(BikeIsExist == null)
			{
				return StatusCode(StatusCodes.Status400BadRequest, new Response { StatusCode = 400, Message = "BikeId is Not Valid or Not Found!" });
			}

			try
			{
				var userId = _userManager.GetUserId(User);
				like.UserId= userId;
				like.BikeId = id;
				var isAlradyLiked = _unitOfWork.BikeLikes.Find(x => x.UserId == like.UserId && x.BikeId == like.BikeId).FirstOrDefault();
				if(isAlradyLiked != null) 
				{
					_unitOfWork.BikeLikes.Delete(isAlradyLiked);
					_unitOfWork.Save();
					return StatusCode(StatusCodes.Status200OK, new Response { StatusCode = 200, Message="Bike Disliked"});
				}
				var res = _unitOfWork.BikeLikes.Create(like);
				_unitOfWork.Save();
				if (res)
				{
					return StatusCode(StatusCodes.Status200OK, new Response { StatusCode = 200, Message = "Bike Liked" });
				}
				return StatusCode(StatusCodes.Status206PartialContent, new Response { StatusCode = 206, Message = "Something Wrong!" });
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost]
		[Route("comment/{id}")]
		public IActionResult AddComment(int id,BikeComment comment)
		{
			var BikeIsExist = _unitOfWork.Bikes.Find(x => x.Id == id).FirstOrDefault();

			if (id == 0)
			{
				return StatusCode(StatusCodes.Status206PartialContent, new Response { StatusCode = 206, Message = "BikeId is Required!" });
			}

			if (BikeIsExist == null)
			{
				return StatusCode(StatusCodes.Status400BadRequest, new Response { StatusCode = 400, Message = "BikeId is Not Valid or Not Found!" });
			}

			try
			{
				var userId = _userManager.GetUserId(User);
				comment.UserId = userId;
				comment.BikeId = id;
				//var isAlradyExist = _unitOfWork.Likes.Find(x => x.UserId == comment.UserId && x.BikeId == comment.BikeId).FirstOrDefault();
				//if (isAlradyLiked != null)
				//{
				//	_unitOfWork.Likes.Delete(isAlradyLiked);
				//	_unitOfWork.Save();
				//	return StatusCode(StatusCodes.Status200OK, new Response { StatusCode = 200, Message = "Bike Disliked" });
				//}

				var res = _unitOfWork.BikeComments.Create(comment);
				_unitOfWork.Save();
				if (res)
				{
					return StatusCode(StatusCodes.Status200OK, new Response { StatusCode = 200, Message = "Bike Comment Done!" });
				}
				return StatusCode(StatusCodes.Status206PartialContent, new Response { StatusCode = 206, Message = "Something Wrong!" });
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet]
		[Route("recentRegistered")]
		public IActionResult MostRecentRegistered()
		{
			try
			{
				var result = _unitOfWork.Bikes.GetAll().Select(x => new
				{
					x.Id,
					x.Name,
					x.Description,
					x.Company,
					x.BikeTypeId,
					Type = x.BikeType.Title,
					likes = x.BikeLikes.Count,
					Comments = x.BikeComments.Select(x => x.Comment)
				}).OrderByDescending(x => x.Id).ToList();
				return StatusCode(StatusCodes.Status200OK, new { StatusCode = 200, Message = "Result Ok", Bikes = result });
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet]
		[Route("popular")]
		public IActionResult MostLiked()
		{
			try
			{
				var result = _unitOfWork.Bikes.GetAll().Select(x => new
				{
					x.Id,
					x.Name,
					x.Description,
					x.Company,
					x.BikeTypeId,
					Type = x.BikeType.Title,
					likes = x.BikeLikes.Count,
					Comments = x.BikeComments.Select(x => x.Comment)
				}).OrderByDescending(x => x.likes).ToList();

				return StatusCode(StatusCodes.Status200OK, new { StatusCode = 200, Message = "Result Ok", Bikes = result });
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
