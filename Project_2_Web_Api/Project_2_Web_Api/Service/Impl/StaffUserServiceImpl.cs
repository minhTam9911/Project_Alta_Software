

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_2_Web_Api.DTO;
using Project_2_Web_Api.Helplers;
using Project_2_Web_API.Models;

namespace Project_2_Web_Api.Service.Impl;

public class StaffUserServiceImpl : StaffUserService
{
	private DatabaseContext db;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper mapper;
	private readonly IConfiguration configuration;
	public StaffUserServiceImpl(DatabaseContext db,IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper)
	{
		this.mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
		this.db = db;
		this.configuration = configuration;
	}

	public Task<dynamic> FindById(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<IActionResult> Create(StaffUserDTO staffUserDto)
	{
		var modelState = _httpContextAccessor.HttpContext?.Items["MS_ModelState"] as ModelStateDictionary;
		var staff = mapper.Map<StaffUser>(staffUserDto);
		try
		{

			if (modelState != null && !modelState.IsValid)
			{
				return new BadRequestObjectResult(modelState);
			}
			else
			{
				if (await db.Users.FirstOrDefaultAsync(x => x.Email == staff.Email) != null)
				{
					return new BadRequestObjectResult(new { error = "Email already exist!!" });
				}
				if (await db.StaffUsers.FirstOrDefaultAsync(x => x.Email == staff.Email) != null)
				{
					return new BadRequestObjectResult(new { error = "Email already exist!!" });
				}
				if (await db.Distributors.FirstOrDefaultAsync(x => x.Email == staff.Email) != null)
				{
					return new BadRequestObjectResult(new { error = "Email already exist!!" });
				}
				if (await db.Positions.FindAsync(staff.PositionId) == null)
				{
					return new BadRequestObjectResult(new { error = "Position not exist!!" });
				}

				staff.CreatedDate = DateTime.Now;
				var password = RandomHelper.RandomDefaultPassword(8);
				var mailHelper = new MailHelper(configuration);

				var content = "<h2>CDExcellent</h2><br><br>" +
							"<h3>Hello " + staff.Fullname + "!</h3><br>" +
							"<h5>CDExcellent is glad you signed up.</h5><br>" +
							"<h5>=>Your account: " + staff.Email + ".</h5><br>" +
							"<h5>=>Password: " + password + ".</h5><br>" +
							"<h5>This is only a temporary password, please log in and change it.</h5><br>" +
							"<h5>Thank you very much!</h5>";
				var check = mailHelper.Send(configuration["Gmail:Username"], staff.Email, "Welcome " + staff.Fullname + "to join CDExcellent", content);
				if (!check)
				{
					return new BadRequestObjectResult(new { error = "Email sending failed." });
				}
				var hashPassword = BCrypt.Net.BCrypt.HashPassword(password);
				staff.Password = hashPassword;
				db.StaffUsers.Add(staff);
				if (await db.SaveChangesAsync() > 0)
				{
					return new OkObjectResult(new { msg = "Added successfully !!" });
				}
				else
				{
					return new BadRequestObjectResult(new { error = "Added failure !!" });
				}

			}
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}

	public async Task<IActionResult> Update(string id, StaffUserDTO staffUserDto)
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
				return new { error = "Data is null !!!" };
			}
			return await db.StaffUsers.Select(x => new
			{
				id = x.Id,
				fullname = x.Fullname,
				email = x.Email,
				area = x.Area.Name
			}).ToListAsync();
		}
		catch (Exception ex)
		{
			return new { error = ex.Message };
		}
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
