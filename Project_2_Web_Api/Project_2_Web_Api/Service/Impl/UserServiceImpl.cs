

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_2_Web_Api.DTO;
using Project_2_Web_API.Models;

namespace Project_2_Web_Api.Service.Impl;

public class UserServiceImpl : UserService
{
	private DatabaseContext db;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper mapper;
	public UserServiceImpl(DatabaseContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper)
	{
		this.mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
		this.db = db;
	}

	public Task<dynamic> FindById(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<IActionResult> Create(StaffUserDTO staffUserDto)
	{
		var modelState = _httpContextAccessor.HttpContext?.Items["MS_ModelState"] as ModelStateDictionary;
		var staffUser = mapper.Map<StaffUser>(staffUserDto);
		try
		{
			
			if(modelState != null && !modelState.IsValid)
			{
				return new BadRequestObjectResult(modelState);
			}
			else
			{
				if(await db.StaffUsers.FirstOrDefaultAsync(x=>x.Email == staffUser.Email) != null)
				{
					return new BadRequestObjectResult(new {error = "Email already exist!!"});
				}
				if (await db.Positions.FindAsync(staffUser.PositionId) == null)
				{
					return new BadRequestObjectResult(new { error = "Position not exist!!" });
				}
				else
				{
					staffUser.CreatedDate = DateTime.Now;
					var hashPassword = BCrypt.Net.BCrypt.HashPassword(staffUser.Password);
					staffUser.Password = hashPassword;
					staffUser.IsStatus = false;
					staffUser.TokenRefresh = "unactive";
					db.StaffUsers.Add(staffUser);
					if (await db.SaveChangesAsync() > 0)
					{
						return new OkObjectResult(new { msg =  "Added successfully !!"});
					}
					else
					{
						return new BadRequestObjectResult(new { error = "Added failure !!" });
					}
				}
			}
		}
		catch(Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}

	public async Task<IActionResult> Update(Guid id, StaffUserDTO staffUserDto)
	{
		throw new NotImplementedException();
	}

	public Task<IActionResult> Delete(string id)
	{
		throw new NotImplementedException();
	}

	public async Task<dynamic> FindAll()
	{
		try
		{

			if (await db.StaffUsers.ToListAsync() == null)
			{
				return new BadRequestObjectResult(new { error = "Data is null !!!" });
			}
			return await db.StaffUsers.Select(x => new
			{
				id = x.Id,
				fullname = x.Fullname,
				email = x.Email
			}).ToListAsync();
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}
}
