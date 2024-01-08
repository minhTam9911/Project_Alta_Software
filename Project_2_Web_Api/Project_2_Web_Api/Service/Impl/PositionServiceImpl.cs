using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
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

	public async Task<IActionResult> Create(Position position)
	{
		var modelState = httpContextAccessor.HttpContext?.Items["MS_ModelState"] as ModelStateDictionary;
		try
		{
			if (position == null)
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

					if (db.Positions.FirstOrDefault(x => x.Name.ToLower() == position.Name.ToLower()) != null)
					{
						return new BadRequestObjectResult(new { msg = "Name Position already exist!" });
					}
					position.Created = DateTime.Now;
					db.Positions.Add(position);
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
		catch(Exception ex)
		{
			return new BadRequestObjectResult(new { msg = ex.Message });
		}
	}

	public async Task<IActionResult> Update(int id, Position position)
	{
		var modelState = httpContextAccessor.HttpContext?.Items["MS_ModelState"] as ModelStateDictionary;
		try
		{
			if (position == null)
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
					if(await db.Positions.FindAsync(id) == null)
					{
						return new BadRequestObjectResult(new { msg = "Id does not exist! " });
					}
					
					if (await db.Positions.Where(x => x.Name == position.Name && x.Id != id).AnyAsync())
					{
							return new BadRequestObjectResult(new { msg = "Name Position already exist!" });
					}
					var data = await db.Positions.FindAsync(id);
					data.Name = position.Name;
					data.PositionGroupId = position.PositionGroupId;
					db.Entry(data).State = EntityState.Modified;
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
			return new BadRequestObjectResult(new { msg = ex.Message });
		}
	}

	public async Task<IActionResult> Delete(int id)
	{
		try
		{
			if (await db.Positions.FindAsync(id) == null)
			{
				return new BadRequestObjectResult(new { msg = "Id does not exist!" });
			}
			else
			{
				db.Positions.Remove(await db.Positions.FindAsync(id));
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
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { msg = ex });
		}
	}

	public async Task<dynamic> FindAll()
	{

		try
		{
			var positions = await db.Positions.ToListAsync();

			if (positions == null)
			{
				return new OkObjectResult(new { msg = "The table is empty/null!!" });
			}
			else
			{
				return await db.Positions.Select( x => new {
					id = x.Id,
					name = x.Name,
					positionGroupId = x.PositionGroupId,
					positionGroupName = x.PositionGroup.Name,
					created = x.Created
				}).OrderBy(x=>x.positionGroupId).ToListAsync();
			}
		}
		catch (Exception ex)
		{
			return ex.Message;
		}
	}

	public async Task<dynamic> FindById(int id)
	{
		try
		{
			var positions = await db.Positions.FirstOrDefaultAsync(x=>x.Id ==id);

			if (positions == null)
			{
				return new OkObjectResult(new { msg = "The table is empty/null!!" });
			}
			else
			{
				return await db.Positions.Where(i=>i.Id == id).Select(x => new {
					id = x.Id,
					name = x.Name,
					positionGroupId = x.PositionGroupId,
					positionGroupName = x.PositionGroup.Name,
					created = x.Created
				}).FirstOrDefaultAsync();
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
			var positions = await db.Positions.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();

			if (positions == null)
			{
				return new OkObjectResult(new { msg = "The table is empty/null!!" });
			}
			else
			{
				return await db.Positions.Where(x => x.Name.ToLower().Contains(name.ToLower())).Select(x => new
				{
					id = x.Id,
					name = x.Name,
					positionGroupId = x.PositionGroupId,
					positionGroupName = x.PositionGroup.Name,
					created = x.Created
				}).ToListAsync();
			}
		}
		catch (Exception ex)
		{
			return ex.Message;
		}
	}
}
