using Microsoft.AspNetCore.Mvc;
using Project_2_Web_API.Models;

namespace Project_2_Web_Api.Service.Impl;

public class PositionServiceImpl : PositionService
{
	private DatabaseContext db;
	private readonly IHttpContextAccessor httpContextAccessor;
	public PositionServiceImpl(DatabaseContext db, IHttpContextAccessor httpContextAccessor)
	{
		this.db = db;
		this.httpContextAccessor = httpContextAccessor;
	}

	public Task<IActionResult> Create(Position position)
	{
		throw new NotImplementedException();
	}

	public Task<IActionResult> Update(int id, Position position)
	{
		throw new NotImplementedException();
	}

	public Task<IActionResult> Delete(int id)
	{
		throw new NotImplementedException();
	}

	public Task<dynamic> FindAll()
	{
		throw new NotImplementedException();
	}

	public Task<dynamic> FindById(string id)
	{
		throw new NotImplementedException();
	}

	public Task<dynamic> FindByName(string name)
	{
		throw new NotImplementedException();
	}
}
