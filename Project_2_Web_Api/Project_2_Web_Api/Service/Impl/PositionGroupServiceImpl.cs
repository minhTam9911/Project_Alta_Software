using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_2_Web_API.Models;

namespace Project_2_Web_Api.Service.Impl;

public class PositionGroupServiceImpl : PositionGroupService
{
	private DatabaseContext db;
	private readonly IHttpContextAccessor httpContextAccessor;
	public PositionGroupServiceImpl(DatabaseContext db, IHttpContextAccessor httpContextAccessor)
	{
		this.db = db;
		this.httpContextAccessor = httpContextAccessor;
	}

	/*public async Task<IActionResult> Create(string positionGroup, string role)
	{
		var positionGroupGet = new PositionGroup();
		var modelState = httpContextAccessor.HttpContext?.Items["MS_ModelState"] as ModelStateDictionary;
		try
		{
			if (positionGroup == null)
			{
				return new BadRequestObjectResult(new { msg = "Data request is null!!!" });
			}
			else
			{
				positionGroupGet = JsonConvert.DeserializeObject<PositionGroup>(positionGroup);
				if (modelState != null && !modelState.IsValid)
				{
					return new BadRequestObjectResult(modelState);
				}
				else
				{
					if (role.ToLower() != "administrator" && role.ToLower() != "owner")
					{
						return new UnauthorizedObjectResult(new { msg = "You do not have sufficient permissions to access this feature" });
					}
					else
					{
						if (db.PositionGroups.FirstOrDefault(x => x.Name.ToLower() == positionGroupGet.Name.ToLower()) != null)
						{
							return new BadRequestObjectResult(new { msg = "Name Position Group already exist!" });
						}
						positionGroupGet.Created = DateTime.Now;
						db.PositionGroups.Add(positionGroupGet);
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
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { msg = ex.Message });
		}
	}
*/
	
	public async Task<IActionResult> Delete(string id)
	{
		try
		{
			if(await db.PositionGroups.FindAsync(Int32.Parse(id)) == null)
			{
				return new BadRequestObjectResult(new { msg = "id does not exist!" });
			}
			else
			{
				db.PositionGroups.Remove(await db.PositionGroups.FindAsync(Int32.Parse(id)));
				var check = db.SaveChangesAsync();
				if (await check > 0)
				{
					return new OkObjectResult(new { msg = "Delete Successfully!" });
				}
				else
				{
					return new BadRequestObjectResult(new { msg = "Delete Failed!" });
				}
			}
		}catch(Exception ex)
		{
			return new BadRequestObjectResult(new { msg = ex });
		}
	}

	public async Task<dynamic> FindAll()
	{
		try
		{
			var positionGroups = await db.PositionGroups.ToListAsync();

			if (positionGroups == null)
			{
				return new OkObjectResult(new { msg = "The table is empty/null!!" });
			}
			else
			{
				return await db.PositionGroups.Select(x=>new {
					id = x.Id, name = x.Name,
				}).ToListAsync();
			}
		}
		catch(Exception ex)
		{
			return ex.Message;
		}
	}

	public async Task<dynamic> FindById(string id)
	{

		try
		{
			if (await db.PositionGroups.FirstOrDefaultAsync(x=>x.Id == Int32.Parse(id)) == null)
			{
				return new OkObjectResult(new { msg = "The table is empty/null !!" });
			}
			else
			{
				dynamic data = db.PositionGroups.Where(i=>i.Id == Int32.Parse(id)).Select(x => new {
					id = x.Id,
					name = x.Name,
				}).FirstOrDefaultAsync();
				return await data;
			}
		}
		catch (Exception ex)
		{
			return ex.Message;
		}
	}

	public async Task<dynamic> FindByName(string name)
	{

		try
		{
			if ( await db.PositionGroups.ToListAsync() == null)
			{
				return new OkObjectResult(new { msg = "The table is empty/null !!" });
			}
			else
			{
				dynamic data = db.PositionGroups.Where(i=>i.Name.ToLower().Contains(name.ToLower())).Select(x => new {
					id = x.Id,
					name = x.Name,
				}).ToListAsync();
				return await data;
			}
		}
		catch (Exception ex)
		{
			return ex.Message;
		}
	}

	public async Task<IActionResult> Create(PositionGroup positionGroup)
	{

		var modelState = httpContextAccessor.HttpContext?.Items["MS_ModelState"] as ModelStateDictionary;
		try
		{
			if (positionGroup == null)
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
					
						if (db.PositionGroups.FirstOrDefault(x => x.Name.ToLower() == positionGroup.Name.ToLower()) != null)
						{
							return new BadRequestObjectResult(new { msg = "Name Position Group already exist!" });
						}
						positionGroup.Created = DateTime.Now;
						db.PositionGroups.Add(positionGroup);
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

	public async Task<IActionResult> Update(string id, PositionGroup positionGroup)
	{

		var modelState = httpContextAccessor.HttpContext?.Items["MS_ModelState"] as ModelStateDictionary;
		try
		{
			if (positionGroup == null)
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

					if (db.PositionGroups.FirstOrDefault(x => x.Name.ToLower() == positionGroup.Name.ToLower()) != null)
					{
						return new BadRequestObjectResult(new { msg = "Name Position Group already exist!" });
					}
					positionGroup.Id = Int32.Parse(id);
					db.Entry(positionGroup).State = EntityState.Modified;
					int check = await db.SaveChangesAsync();
					if (check > 0)
					{
						return new OkObjectResult(new { msg = "Update successfully!" });
					}
					else
					{
						return new BadRequestObjectResult(new { msg = "Update failure!" });
					}


				}
			}
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { msg = ex });
		}
	}
}