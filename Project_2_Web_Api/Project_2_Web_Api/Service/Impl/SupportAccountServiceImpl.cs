using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_2_Web_Api.Helplers;
using Project_2_Web_API.Models;
using System.Net.Mail;

namespace Project_2_Web_Api.Service.Impl;

public class SupportAccountServiceImpl : SupportAccountService
{

	private DatabaseContext db;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper mapper;
	private readonly IConfiguration configuration;
	public SupportAccountServiceImpl(DatabaseContext db,IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper)
	{
		this.configuration = configuration;
		this.mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
		this.db = db;
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
			if (data == null)
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
			if (await db.SaveChangesAsync() > 0)
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
			if (email == null)
			{
				return new BadRequestObjectResult(new { error = "Email cannot be empty !!" });
			}
			if (!CheckEmail(email))
			{
				return new BadRequestObjectResult(new { error = "Email invalidate" });
			}
			var security = RandomHelper.RandomInt(6);
			var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
			var staff = await db.StaffUsers.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
			var distributor = await db.Distributors.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
			if (user != null)
			{
				user.SecurityCode = security;
				db.Entry(user).State = EntityState.Modified;
				if (await db.SaveChangesAsync() > 0) ;
				else
				{
					return new BadRequestObjectResult(new { error = "The system encountered a problem !!" });
				}
			}
			else if (staff != null)
			{
				staff.SecurityCode = security;
				db.Entry(staff).State = EntityState.Modified;
				if (await db.SaveChangesAsync() > 0) ;
				else
				{
					return new BadRequestObjectResult(new { error = "The system encountered a problem !!" });
				}
			}
			else if (distributor != null)
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
						"<h5>CDExcellent has received a request to use.</h5>" + email + "<br>" +
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
		catch (Exception ex)
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
				if (user.SecurityCode == code)
				{
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
					return new BadRequestObjectResult(new { error = "Verification code does not match", valid = false });
				}


			}
			else if (staff != null)
			{
				if (staff.SecurityCode == code)
				{
					staff.SecurityCode = null;
					db.Entry(staff).State = EntityState.Modified;
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

			return new OkObjectResult(new { msg = "Valid verification code!", valid = true });

		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
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
		}
		catch (Exception ex)
		{
			return false;
		}
		return valid;
	}

	
}
