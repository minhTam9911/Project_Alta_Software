﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Project_2_Web_Api.DTO;
using Project_2_Web_Api.Helplers;
using Project_2_Web_API.Models;
using System.Net.Mail;
using System;

namespace Project_2_Web_Api.Service.Impl;

public class UserServiceImpl : UserService
{
	private DatabaseContext db;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper mapper;
	private IConfiguration configuration;
	public UserServiceImpl(DatabaseContext db,IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper)
	{
		this.mapper = mapper;
		this.configuration = configuration;
		_httpContextAccessor = httpContextAccessor;
		this.db = db;
	}

	public async Task<IActionResult> Create(UserDTO userDTO)
	{
		var modelState = _httpContextAccessor.HttpContext?.Items["MS_ModelState"] as ModelStateDictionary;
		var user = mapper.Map<User>(userDTO);
		try
		{

			if (modelState != null && !modelState.IsValid)
			{
				return new BadRequestObjectResult(modelState);
			}
			else
			{
				if (await db.Users.FirstOrDefaultAsync(x => x.Email == userDTO.Email) != null)
				{
					return new BadRequestObjectResult(new { error = "Email already exist!!" });
				}
				if (await db.StaffUsers.FirstOrDefaultAsync(x => x.Email == userDTO.Email) != null)
				{
					return new BadRequestObjectResult(new { error = "Email already exist!!" });
				}
				if (await db.Distributors.FirstOrDefaultAsync(x => x.Email == userDTO.Email) != null)
				{
					return new BadRequestObjectResult(new { error = "Email already exist!!" });
				}
				if (await db.Positions.FindAsync(user.PositionId) == null)
				{
					return new BadRequestObjectResult(new { error = "Position not exist!!" });
				}
				var checkPosition = await db.Positions.FindAsync(user.PositionId);
				if(checkPosition.Name.ToLower() != "guest" && checkPosition.Name != "other department")
				{
					return new BadRequestObjectResult(new { error = "Position option is invalid. You can only choose 'Guest' or 'Other Department'" });
				}
				
					user.CreatedDate = DateTime.Now;
					var password = RandomHelper.RandomDefaultPassword(8);
					var mailHelper = new MailHelper(configuration);

					var content = "<h2>CDExcellent</h2><br><br>" +
								"<h2>Hello " + user.FullName + "!</h2><br>" +
							"<h3>CDExcellent is glad you signed up.</h3><br>" +
							"<h2>=>Your account: " + user.Email + "</h2><br>" +
							"<h2>=>Password: " + password + "</h2><br>" +
							"<h3>This is only a temporary password, please log in and change it.</h3><br>" +
							"<h2>Thank you very much!</h2>";
				var check = mailHelper.Send(configuration["Gmail:Username"],user.Email,"Welcome "+user.FullName+ "to join CDExcellent", content);
					if (!check)
					{
						return new BadRequestObjectResult(new { error = "Email sending failed." });
					}
					var hashPassword = BCrypt.Net.BCrypt.HashPassword(password);
					user.Password = hashPassword;
					db.Users.Add(user);
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

	public async Task<IActionResult> Update(string id, UserDTO userDTO)
	{
		var modelState = _httpContextAccessor.HttpContext?.Items["MS_ModelState"] as ModelStateDictionary;
		var user = mapper.Map<User>(userDTO);
		Guid idUser;
		bool parseGuid = Guid.TryParse(id, out idUser);
		try
		{
			if (parseGuid == false)
			{
				return new BadRequestObjectResult(new { error = "Id User invalid !!" });
			}
			if (modelState != null && !modelState.IsValid)
			{
				return new BadRequestObjectResult(modelState);
			}
			else
			{
				if (await db.Users.FirstOrDefaultAsync(x => x.Email == userDTO.Email && x.Id != idUser) != null)
				{
					return new BadRequestObjectResult(new { error = "Email already exist!!" });
				}
				if (await db.StaffUsers.FirstOrDefaultAsync(x => x.Email == userDTO.Email && x.Id != idUser) != null)
				{
					return new BadRequestObjectResult(new { error = "Email already exist!!" });
				}
				if (await db.Positions.FindAsync(user.PositionId) == null)
				{
					return new BadRequestObjectResult(new { error = "Position not exist!!" });
				}
				else
				{
					var data = await db.Users.FindAsync(idUser);
					data.IsStatus = user.IsStatus;
					data.FullName = user.FullName;
					data.Email = user.Email;
					data.PositionId = user.PositionId;
					db.Entry(data).State = EntityState.Modified;
					if (await db.SaveChangesAsync() > 0)
					{
						return new OkObjectResult(new { msg = "Update successfully !!" });
					}
					else
					{
						return new BadRequestObjectResult(new { error = "Update failure !!" });
					}
				}
			}
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}
	public async Task<IActionResult> Delete(string id)
	{
		Guid idUser;
		bool parseGuid = Guid.TryParse(id, out idUser);
		try
		{
			if (parseGuid == false)
			{
				return new BadRequestObjectResult(new { error = "Id User invalid !!" });
			}
			if (await db.Users.FindAsync(idUser) == null)
			{
				return new BadRequestObjectResult(new { error = "Id does not exist!" });
			}
			else
			{
				db.Users.Remove(await db.Users.FindAsync(idUser));
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

			if (await db.Users.AnyAsync() == false)
			{
				return new { error = "Data is null !!!" };
			}
			return await db.Users.Select(x => new
			{
				id = x.Id,
				fullname = x.FullName,
				email = x.Email,
				positionId = x.PositionId,
				positionName = x.Position.Name,
				areaId = x.Area.Id,
				areaName=x.Area.Name,
				status = x.IsStatus,
				createBy = x.StaffUser.Fullname
				
			}).ToListAsync();
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}

	public async Task<dynamic> FindById(string id)
	{
		Guid idUser;
		bool parseGuid = Guid.TryParse(id, out idUser);
		try
		{
			if (parseGuid == false)
			{
				return new BadRequestObjectResult(new { error = "Id User invalid !!" });
			}
			if (await db.Users.AnyAsync() == false || await db.Users.FindAsync(idUser) == null)
			{
				return new { error = "Data is null !!!" };
			}
			return await db.Users.Where(x=>x.Id == idUser).Select(x => new
			{
				id = x.Id,
				fullname = x.FullName,
				email = x.Email,
				positionId = x.PositionId,
				positionName = x.Position.Name,
				areaId = x.Area.Id,
				areaName = x.Area.Name,
				status = x.IsStatus,
				createBy = x.StaffUser.Fullname

			}).FirstOrDefaultAsync();
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}

	public async Task<dynamic> FindByName(string name)
	{
		try
		{
			
			if (await db.Users.AnyAsync() == false
				|| await db.Users.Where(x => x.FullName.ToLower().Contains(name.ToLower())).AnyAsync() == false)
			{
				return new { error = "Data is null !!!" };
			}
			return await db.Users.Where(x => x.FullName.ToLower().Contains(name.ToLower())).Select(x => new
			{
				id = x.Id,
				fullname = x.FullName,
				email = x.Email,
				positionId = x.PositionId,
				positionName = x.Position.Name,
				areaId = x.Area.Id,
				areaName = x.Area.Name,
				status = x.IsStatus,
				createBy = x.StaffUser.Fullname

			}).ToListAsync();
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}

	public async Task<IActionResult> SettingPermission(string id, int[] permissions)
	{
		Guid idUser;
		bool parseGuid = Guid.TryParse(id, out idUser);
		try
		{
			if (parseGuid == false)
			{
				return new BadRequestObjectResult(new { error = "Id User invalid !!" });
			}
			var data = await db.Users.FindAsync(idUser);
			if (data == null)
			{
				return new BadRequestObjectResult(new { error = "Id User does not exist !!" });
			}
			var permissionDB = await db.GrantPermissions.ToListAsync();
			foreach(var permision in permissions)
			{
				if(permissionDB.Any(x=>x.Id == permision))
				{
					data.GrantPermissions.Add( await db.GrantPermissions.FindAsync(permision));
					db.Entry(data).State = EntityState.Modified;
					if (await db.SaveChangesAsync() > 0) ;
					else
					{
						return new BadRequestObjectResult(new { error = "The system encountered a problem !!" });
					}
				}
				else
				{
					return new BadRequestObjectResult(new { error = "ID Permission does not exist !!" });
				}
			}
			return new OkObjectResult(new { msg = "Add permissions to user successfully" });
		}
		catch(Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}

}
