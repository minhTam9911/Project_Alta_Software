using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project_2_Web_API.Models;

namespace Project_2_Web_Api.Service.Impl;

public class GrantPermissionServiceImpl : GrantPermissionService
{
	private DatabaseContext db;
	private readonly IHttpContextAccessor httpContextAccessor;
	public GrantPermissionServiceImpl(DatabaseContext db, IHttpContextAccessor httpContextAccessor)
	{
		this.db = db;
		this.httpContextAccessor = httpContextAccessor;
	}

	public async Task<IActionResult> Create(GrantPermission grantPermission)
	{
		var modelState = httpContextAccessor.HttpContext?.Items["MS_ModelState"] as ModelStateDictionary;
		try
		{
			if (grantPermission == null)
			{
				return new BadRequestObjectResult(new { msg = "Data request is null!!!" });
			}
			else
			{

				if (modelState != null && !modelState.IsValid)
				{
					return new BadRequestObjectResult(modelState);
				}
				else
				{

					if (db.GrantPermissions.FirstOrDefault(x => x.Permission.ToLower() == grantPermission.Permission.ToLower()) != null)
					{
						return new BadRequestObjectResult(new { msg = "Permission name already exist!" });
					}
					db.GrantPermissions.Add(grantPermission);
					int check = await db.SaveChangesAsync();
					if (check > 0)
					{
						return new OkObjectResult(new { msg = "Added successfully!" });
					}
					else
					{
						return new BadRequestObjectResult(new { msg = "Added failure!" });
					}


				}
			}
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { msg = ex.Message });
		}
	}

	public Task<IActionResult> Update(int id, GrantPermission grantPermission)
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

	public Task<dynamic> FindById(int id)
	{
		throw new NotImplementedException();
	}

	public Task<dynamic> FindByName(string name)
	{
		throw new NotImplementedException();
	}
}
