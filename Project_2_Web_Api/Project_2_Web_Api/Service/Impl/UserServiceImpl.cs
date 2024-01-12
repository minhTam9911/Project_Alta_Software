using AutoMapper;
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
	public UserServiceImpl(DatabaseContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper)
	{
		this.mapper = mapper;
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
				if (await db.Positions.FindAsync(user.PositionId) == null)
				{
					return new BadRequestObjectResult(new { error = "Position not exist!!" });
				}
				else
				{
					user.CreatedDate = DateTime.Now;
					var password = RandomHelper.RandomDefaultPassword(8);
					var mailHelper = new MailHelper(configuration);
					var content = "<h2>CDExcellent</h2><br><br>" +
								"<h3>Hello " + user.FullName + "!</h3><br>" +
								"<h5>CDExcellent is glad you signed up.</h5><br>" +
								"<h5>=>Your account: " + user.Email + ".</h5><br>" +
								"<h5>=>Password: " + password + ".</h5><br>" +
								"<h5>This is only a temporary password, please log in and change it.</h5><br>" +
								"<h5>Thank you very much!</h5>";
					var check = mailHelper.Send(configuration["Gmail:Username"],user.Email,"Welcome "+user.FullName+ "to join CDExcellent", content);
					if (!check)
					{
						return new BadRequestObjectResult(new { error = "Email sending failed." });
					}
					var hashPassword = BCrypt.Net.BCrypt.HashPassword(password);
					user.Password = hashPassword;
					user.IsStatus = false;
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

	public async Task<IActionResult> ChangePassword(string id, string oldPassword, string newPassword)
	{
		Guid idUser;
		bool parseGuid = Guid.TryParse(id, out idUser);
		try
		{
			if (parseGuid == false)
			{
				return new BadRequestObjectResult(new { error = "Id user invalid !!" });
			}
			var data = await db.Users.FindAsync(idUser);
			if(data == null)
			{
				return new BadRequestObjectResult(new { error = "Id does not exist !!" });
			}
			if (!BCrypt.Net.BCrypt.Verify(oldPassword, data.Password))
			{
				return new BadRequestObjectResult(new { error = "The old password does not match the new password!!" });
			}
			var hashPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
			data.Password = hashPassword;
			db.Entry(data).State = EntityState.Modified;
			if(await db.SaveChangesAsync() > 0)
			{
				return new OkObjectResult(new { msg = "Update Password successfully !!!" });
			}
			else
			{
				return new BadRequestObjectResult(new { error = "Update Password failure !!" });
			}
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}

	public async Task<IActionResult> ForgotPassword(string? email)
	{
		try
		{
			if(email == null)
			{
				return  new BadRequestObjectResult(new { error = "Email cannot be empty !!" });
			}
			if (!CheckEmail(email))
			{
				return new BadRequestObjectResult(new {error = "Email invalidate" });
			}
			var security = RandomHelper.RandomInt(6);
			var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(x=>x.Email == email);
			var staff = await db.StaffUsers.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
			var distributor = await db.Distributors.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
			if(user != null)
            {
				user.SecurityCode = security;
				db.Entry(user).State = EntityState.Modified;
				if (await db.SaveChangesAsync() > 0) ;
				else
				{
					return new BadRequestObjectResult(new { error = "The system encountered a problem !!" });
				}
			}
			else if(staff != null)
			{
				staff.SecurityCode = security;
				db.Entry(staff).State = EntityState.Modified;
				if (await db.SaveChangesAsync() > 0) ;
				else
				{
					return new BadRequestObjectResult(new { error = "The system encountered a problem !!" });
				}
			}
			else if(distributor != null) 
			{
				distributor.SecurityCode = security;
				db.Entry(distributor).State = EntityState.Modified;
				if (await db.SaveChangesAsync() > 0) ;
				else
				{
					return new BadRequestObjectResult(new { error = "The system encountered a problem !!" });
				}
			}
			else
			{
				return new BadRequestObjectResult(new { error = "Email does not exist" });
			}
			var mailHelper = new MailHelper(configuration);
			var content = "<h2>Verify Your Recovery  Email</h2><br><br>" +
						"<h5>CDExcellent has received a request to use.</h5>"+email+"<br>" +
						"<h5>Use this code to complete password recovery./h5><br>" +
						"<h3>=>Security Code: " + security + ".</h3><br>" +
						"<h5>Thank you very much!</h5>";
			var check = mailHelper.Send(configuration["Gmail:Username"], email, "Email verification code", content);
			if (!check)
			{
				return new BadRequestObjectResult(new { error = "Email sending failed." });
			}
			else
			{
				return new OkObjectResult(new { msg = "Please check the email you just entered" });
			}

		}
		catch(Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}

	public async Task<IActionResult> VerifySecurityCode(string email, string code)
	{
		try
		{
			if (email == null)
			{
				return new BadRequestObjectResult(new { error = "Email cannot be empty !!" });
			}
			if (!CheckEmail(email))
			{
				return new BadRequestObjectResult(new { error = "Email invalidate" });
			}
			var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
			var staff = await db.StaffUsers.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
			var distributor = await db.Distributors.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
			if (user != null)
			{
				if(user.SecurityCode == code) {
					user.SecurityCode = null;
					db.Entry(user).State = EntityState.Modified;
					if (await db.SaveChangesAsync() > 0) ;
					else
					{
						return new BadRequestObjectResult(new { error = "The system encountered a problem !!" });
					}
				}
				else
				{
					return new BadRequestObjectResult(new { error = "Verification code does not match" , valid = false });
				}
				
				
			}
			else if (staff != null)
			{
				if (staff.SecurityCode == code)
				{
					staff.SecurityCode = null;
					db.Entry(staff).State = EntityState.Modified;
					if (await db.SaveChangesAsync() > 0);
					else
					{
						return new BadRequestObjectResult(new { error = "The system encountered a problem !!" });
					}
				}
				else
				{
					return new BadRequestObjectResult(new { error = "Verification code does not match", valid = false });
				}
			}
			else if (distributor != null)
			{
				if (distributor.SecurityCode == code)
				{
					distributor.SecurityCode = null;
					db.Entry(distributor).State = EntityState.Modified;
					if (await db.SaveChangesAsync() > 0) ;
					else
					{
						return new BadRequestObjectResult(new { error = "The system encountered a problem !!" });
					}
				}
				else
				{
					return new BadRequestObjectResult(new { error = "Verification code does not match", valid = false });
				}
			}
			else
			{
				return new BadRequestObjectResult(new { error = "Email does not exist" });
			}

			return new OkObjectResult(new { msg = "Valid verification code!",valid = true });

		}
		catch(Exception ex)
		{
			return new BadRequestObjectResult(new {error = ex.Message});
		}
	}

	public async Task<IActionResult> ChangeForgotPassword(string email, string newPassword)
	{
		try
		{
			if (email == null)
			{
				return new BadRequestObjectResult(new { error = "Email cannot be empty !!" });
			}
			if (!CheckEmail(email))
			{
				return new BadRequestObjectResult(new { error = "Email invalidate" });
			}
			var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
			var staff = await db.StaffUsers.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
			var distributor = await db.Distributors.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
			var hashPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
			if (user != null)
			{
				user.Password = hashPassword;
				db.Entry(user).State = EntityState.Modified;
				if (await db.SaveChangesAsync() > 0) ;
				else
				{
					return new BadRequestObjectResult(new { error = "The system encountered a problem !!" });
				}
			}
			else if (staff != null)
			{
				staff.Password = hashPassword;
				db.Entry(staff).State = EntityState.Modified;
				if (await db.SaveChangesAsync() > 0) ;
				else
				{
					return new BadRequestObjectResult(new { error = "The system encountered a problem !!" });
				}
			}
			else if (distributor != null)
			{
				distributor.Password = hashPassword;
				db.Entry(distributor).State = EntityState.Modified;
				if (await db.SaveChangesAsync() > 0) ;
				else
				{
					return new BadRequestObjectResult(new { error = "The system encountered a problem !!" });
				}
			}
			else
			{
				return new BadRequestObjectResult(new { error = "Email does not exist" });
			}
			
				return new OkObjectResult(new { msg = "Password changed successfully !!" });

		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}
	public bool CheckEmail(string email)
	{
		bool valid = true;
		try
		{
			var emailAddress = new MailAddress(email);
		}catch(Exception ex)
		{
			return false;
		}
		return valid;
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
