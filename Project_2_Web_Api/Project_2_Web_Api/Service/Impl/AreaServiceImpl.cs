using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Project_2_Web_Api.DTO;
using Project_2_Web_API.Models;

namespace Project_2_Web_Api.Service.Impl;

public class AreaServiceImpl : AreaService
{
	private DatabaseContext db;
	private readonly IHttpContextAccessor httpContextAccessor;
	private readonly IMapper mapper;
	public AreaServiceImpl(DatabaseContext db,
		IHttpContextAccessor httpContextAccessor,
		IMapper mapper
		)
	{
		this.db = db;
		this.mapper = mapper;
		this.httpContextAccessor = httpContextAccessor;
	}

	public async Task<IActionResult> Create(AreaDTO areaDTO)
	{
		var area = mapper.Map<Area>(areaDTO);
		var modelState = httpContextAccessor.HttpContext?.Items["MS_ModelState"] as ModelStateDictionary;
		try
		{
			if (modelState != null && !modelState.IsValid)
			{
				return new BadRequestObjectResult(modelState);
			}
			else
			{

				if (await db.Areas.FirstOrDefaultAsync(x => x.Code.ToLower() == area.Code.ToLower()) != null)
				{
					return new BadRequestObjectResult(new { error = "Code already exist!" });
				}
				db.Areas.Add(area);
				int check = await db.SaveChangesAsync();
				if (check > 0)
				{
					return new OkObjectResult(new { msg = "Added successfully!" });
				}
				else
				{
					return new BadRequestObjectResult(new { error = "Added failure!" });
				}
			}
			
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}

	public async Task<IActionResult> Update(int id, string nameArea)
	{ 
		try
		{
			if (nameArea == null)
			{
				return new BadRequestObjectResult(new {error = "Name Area required !!"});
			}
			else
			{
				
				var area = await db.Areas.FindAsync(id);
				if (area == null)
				{
					return  new BadRequestObjectResult(new { error = "Id Area not exist !!" });
				}
					area.Name = nameArea;
				db.Entry(area).State = EntityState.Modified;
				int check = await db.SaveChangesAsync();
				if (check > 0)
				{
					return new OkObjectResult(new { msg = "Update successfully!" });
				}
				else
				{
					return new BadRequestObjectResult(new { error = "Update failure!" });
				}
			}

		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}

	public async Task<IActionResult> AddStaffToArea(int idArea, string _idStaff)
	{
		Guid idStaff;
		bool parseGuid = Guid.TryParse(_idStaff, out idStaff);
		try
		{
			if (parseGuid ==false)
			{
			return new BadRequestObjectResult(new { error = "Id Statff invalid !!" });
			}
			if (await db.StaffUsers.FindAsync(idStaff) == null)
			{
				return new BadRequestObjectResult(new { error = "ID Staff does not exist!" });
			}
			var area = await db.Areas.FirstOrDefaultAsync(x => x.Id == idArea);
			if(area == null)
			{
				return new BadRequestObjectResult(new { error = "ID Area does not exist!" });
			}
			var staff = await db.StaffUsers.FirstOrDefaultAsync(x=>x.Id == idStaff);
			area.StaffUsers.Add(staff);
			db.Entry(area).State = EntityState.Modified;
			int check = await db.SaveChangesAsync();
			if (check > 0)
			{
				return new OkObjectResult(new { msg = "Added successfully!" });
			}
				else
			{
				return new BadRequestObjectResult(new { error = "Added failure!" });
			}
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}

	public Task<IActionResult> RemoveStaffInArea(int idArea, int idStaff)
	{
		throw new NotImplementedException();
	}

	public async Task<IActionResult> Delete(int id)
	{
		try
		{
			if (await db.Areas.FindAsync(id) == null)
			{
				return new BadRequestObjectResult(new { error = "Id does not exist!" });
			}
			else
			{
				db.Areas.Remove(await db.Areas.FindAsync(id));
				var check = await db.SaveChangesAsync();
				if (check > 0)
				{
					return new OkObjectResult(new { msg = "Delete Successfully!" });
				}
				else
				{
					return new BadRequestObjectResult(new { error = "Delete Failed!" });
				}
			}
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}

	public async Task<dynamic> FindAll()
	{
		try
		{

			if(await db.Areas.AnyAsync()==false)
			{
				return new { msg = "Data is null !!!" };
			}
			return await db.Areas.Select(x => new
			{
				id = x.Id,
				code = x.Code,
				name = x.Name,
				staffs = x.StaffUsers.Select(i => new
				{
					id = i.Id,
					fullname = i.Fullname,
					email = i.Email,
					position = i.Position.Name,
					isstatus = i.IsStatus/*==true? "Activated" : "Not activated"*/
				}).ToList(),
				distributors = x.Distributors.Select(j => new
				{
					id=j.Id,
					name = j.Name,
					email = j.Email,
					address = j.Address,
					phone = j.PhoneNumber,
					position = j.Position.Name,
					isstatus = j.IsStatus/* == true ? "Activated" : "Not activated"*/
				})
			}).ToListAsync();
		}catch(Exception ex)
		{
			return new {error =  ex.Message};
		}
	}

	public async Task<dynamic> FindById(int id)
	{
		try
		{

			if (await db.Areas.AnyAsync() == false || await db.Areas.FindAsync(id)==null)
			{
				return new { msg = "Data is null !!!" };
			}
			return await db.Areas.Where(a=>a.Id==id).Select(x => new
			{
				id = x.Id,
				code = x.Code,
				name = x.Name,
				staffs = x.StaffUsers.Select(i => new
				{
					id = i.Id,
					fullname = i.Fullname,
					email = i.Email,
					position = i.Position.Name,
					isstatus = i.IsStatus == true ? "Actived" : "Activated"
				}).ToList(),
				distributors = x.Distributors.Select(j => new
				{
					id = j.Id,
					name = j.Name,
					email = j.Email,
					address = j.Address,
					phone = j.PhoneNumber,
					position = j.Position.Name,
					isstatus = j.IsStatus == true ? "Actived" : "Activated"
				})
			}).FirstOrDefaultAsync();
		}
		catch (Exception ex)
		{
			return new { error = ex.Message };
		}
	}

	public async Task<dynamic> FindByName(string name)
	{
		try
		{

			if (await db.Areas.AnyAsync() == false || await db.Areas.Where(a => a.Name.ToLower().Contains(name.ToLower())).AnyAsync() == false)
			{
				return new { msg = "Data is null !!!" };
			}
			return await db.Areas.Where(a=>a.Name.ToLower().Contains(name.ToLower())).Select(x => new
			{
				id = x.Id,
				code = x.Code,
				name = x.Name,
				staffs = x.StaffUsers.Select(i => new
				{
					id = i.Id,
					fullname = i.Fullname,
					email = i.Email,
					position = i.Position.Name,
					isstatus = i.IsStatus == true ? "Actived" : "Activated"
				}).ToList(),
				distributors = x.Distributors.Select(j => new
				{
					id = j.Id,
					name = j.Name,
					email = j.Email,
					address = j.Address,
					phone = j.PhoneNumber,
					position = j.Position.Name,
					isstatus = j.IsStatus == true ? "Actived" : "Activated"
				})
			}).ToListAsync();
		}
		catch (Exception ex)
		{
			return new { error = ex.Message };
		}
	}

	public Task<IActionResult> AddDistributorToArea(int idArea, int idStaff)
	{
		throw new NotImplementedException();
	}

	public Task<IActionResult> RemoveDistributorInArea(int idArea, int idStaff)
	{
		throw new NotImplementedException();
	}
}
