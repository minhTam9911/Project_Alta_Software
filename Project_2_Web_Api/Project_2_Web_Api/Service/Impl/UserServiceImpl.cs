

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Project_2_Web_API.Models;

namespace Project_2_Web_Api.Service.Impl;

public class UserServiceImpl : UserService
{
	private DatabaseContext db;
	private readonly IHttpContextAccessor _httpContextAccessor;
	public UserServiceImpl(DatabaseContext db)
	{
		this.db = db;
	}

	public Task<IActionResult> FindById(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<IActionResult> Create(string userJson)
	{
		try
		{
			var modelState = _httpContextAccessor.HttpContext?.Items["MS_ModelState"] as ModelStateDictionary;
			if(modelState != null && !modelState.IsValid)
			{
				return new BadRequestObjectResult(modelState);
			}
			else
			{
				return new BadRequestObjectResult(modelState);
			}
		}
		catch(Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}

	public Task<IActionResult> Update(string userJson)
	{
		throw new NotImplementedException();
	}

	public Task<IActionResult> Delete(string id)
	{
		throw new NotImplementedException();
	}

	public Task<IActionResult> FindAll()
	{
		throw new NotImplementedException();
	}
}
