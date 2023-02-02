using BikeApis.Models;
using BikeApis.Repositories.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BikeApis.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class BikeTypesController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;

		public BikeTypesController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		[Route("")]
		public IActionResult GetAll()
		{
			try
			{
				var result = _unitOfWork.BikeTypes.GetAll();
				return StatusCode(StatusCodes.Status200OK, result);
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
				var result = _unitOfWork.BikeTypes.GetById(id);
				if (result != null)
				{
					return StatusCode(StatusCodes.Status200OK, result);
				}
				return StatusCode(StatusCodes.Status404NotFound);
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost]
		[Route("")]
		public IActionResult Add(BikeType model)
		{
			try
			{
				var isExist = _unitOfWork.BikeTypes.Find(x => x.Title == model.Title).FirstOrDefault();
				if (isExist != null)
				{
					return StatusCode(StatusCodes.Status203NonAuthoritative, new Response
					{
						StatusCode = 203,
						Message = "This type is Alrady Exists!"
					});
				}

				var res = _unitOfWork.BikeTypes.Create(model);
				_unitOfWork.Save();
				if (res)
				{
					return StatusCode(StatusCodes.Status201Created, new Response
					{
						StatusCode = 201,
						Message = "Bike Type Added!"
					});
				}
				return StatusCode(StatusCodes.Status203NonAuthoritative, new Response
				{
					StatusCode = 203,
					Message = "Bike Type Not Added Try Again!"
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
				var result = _unitOfWork.BikeTypes.GetById(id);
				if (result != null)
				{
					_unitOfWork.BikeTypes.Delete(result);
					_unitOfWork.Save();
					return StatusCode(StatusCodes.Status200OK, new Response { StatusCode = 200,Message = "BikeType Is Deleted!",BikeType = result });
				}
				return StatusCode(StatusCodes.Status404NotFound, new Response { StatusCode = 404,Message = "Type Not Found!"});
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPut]
		[Route("{id}")]
		public IActionResult Update(int? id,BikeType model)
		{
			try
			{
				var isExist = _unitOfWork.BikeTypes.Find(x => x.Id == id).FirstOrDefault();
				if (isExist == null)
				{
					return StatusCode(StatusCodes.Status204NoContent, new Response
					{
						StatusCode = 204,
						Message = "This type is Not Exists!"
					});
				}

				isExist.Title = model.Title;
				isExist.Description = model.Description;

				var res = _unitOfWork.BikeTypes.Update(isExist);
				_unitOfWork.Save();
				if (res != null)
				{
					return StatusCode(StatusCodes.Status200OK, new Response
					{
						StatusCode = 200,
						Message = "Bike Type Updated!",
						BikeType = res				
					});
				}
				return StatusCode(StatusCodes.Status203NonAuthoritative, new Response
				{
					StatusCode = 203,
					Message = "Bike Type Not Updated Try Again!"
				});
			}
			catch (System.Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
