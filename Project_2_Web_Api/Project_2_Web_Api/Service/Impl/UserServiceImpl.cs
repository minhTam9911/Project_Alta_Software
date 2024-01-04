
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Project_2_Web_API.Models;


namespace Project_2_Web_Api.Service.Impl;

public class UserServiceImpl : UserService
{
	private DatabaseContext db;

/*	public IActionResult Create(string UserJson)
	{
		try
		{
			
			
		}
		catch(Exception ex)
		{
			
		}
	}*/

	public Task<IActionResult> Delete(string id)
	{
		throw new NotImplementedException();
	}

	public Task<IActionResult> FindAll()
	{
		throw new NotImplementedException();
	}

	public Task<IActionResult> Update(string UserJson)
	{
		throw new NotImplementedException();
	}

	Task<IActionResult> UserService.Create(string UserJson)
	{
		throw new NotImplementedException();
	}
}
