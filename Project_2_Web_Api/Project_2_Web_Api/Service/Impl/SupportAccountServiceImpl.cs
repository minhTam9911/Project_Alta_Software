using AutoMapper;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_2_Web_Api.DTO;
using Project_2_Web_Api.Helplers;
using Project_2_Web_API.Models;
using System.Net.Mail;
using System.Security.Cryptography;

namespace Project_2_Web_Api.Service.Impl;

public class SupportAccountServiceImpl : SupportAccountService
{

	private DatabaseContext db;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper mapper;
	private readonly IConfiguration configuration;
	private readonly UserServiceAccessor userServiceAccessor;
	public SupportAccountServiceImpl(DatabaseContext db,IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserServiceAccessor userServiceAccessor)
	{
		this.configuration = configuration;
		this.mapper = mapper;
		_httpContextAccessor = httpContextAccessor;
		this.db = db;
		this.userServiceAccessor = userServiceAccessor;
	}

	public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
	{
		try
		{

			if (await userServiceAccessor.IsGuest())
			{
				var data = await db.Users.FindAsync(await userServiceAccessor.GetById());
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
			else if (await userServiceAccessor.IsDistributor())
			{
				var data = await db.Distributors.FindAsync(await userServiceAccessor.GetById());
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
			else
			{
				var data = await db.StaffUsers.FindAsync( await userServiceAccessor.GetById());
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
			string? token = null;
			if (user != null)
			{
				user.PasswordResetToken = RandomToken();
				user.ResetTokenExpires = DateTime.Now.AddHours(1);
				user.SecurityCode = security;
				db.Entry(user).State = EntityState.Modified;
				if (await db.SaveChangesAsync() > 0)
				{
					token = user.PasswordResetToken;
				}
				else
				{
					return new BadRequestObjectResult(new { error = "The system encountered a problem !!" });
				}
			}
			else if (staff != null)
			{
				staff.SecurityCode = security;
				staff.PasswordResetToken = RandomToken();
				staff.ResetTokenExpires = DateTime.Now.AddHours(1);
				db.Entry(staff).State = EntityState.Modified;
				if (await db.SaveChangesAsync() > 0)
				{
					token = staff.PasswordResetToken;
				}
				else
				{
					return new BadRequestObjectResult(new { error = "The system encountered a problem !!" });
				}
			}
			else if (distributor != null)
			{
				distributor.SecurityCode = security;
				distributor.PasswordResetToken = RandomToken();
				distributor.ResetTokenExpires = DateTime.Now.AddHours(1);
				db.Entry(distributor).State = EntityState.Modified;
				if (await db.SaveChangesAsync() > 0)
				{
					token = distributor.PasswordResetToken;
				}
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
			
			var check = mailHelper.Send(configuration["Gmail:Username"], email, "Email verification code", MailHelper.HtmlVerify(security));
			if (!check)
			{
				return new BadRequestObjectResult(new { error = "Email sending failed." });
			}
			else
			{
				return new OkObjectResult(new { msg = token });
			}

		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}

	public async Task<IActionResult> VerifySecurityCode(ForgotPasswordRequest forgotPasswordRequest)
	{
		try
		{
			if (forgotPasswordRequest.Token.IsNullOrEmpty())
			{
				return new BadRequestObjectResult(new { error = "TOken is requried !!" });
			}
			var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.PasswordResetToken == forgotPasswordRequest.Token && x.ResetTokenExpires > DateTime.Now);
			var staff = await db.StaffUsers.AsNoTracking().FirstOrDefaultAsync(x => x.PasswordResetToken == forgotPasswordRequest.Token && x.ResetTokenExpires > DateTime.Now);
			var distributor = await db.Distributors.AsNoTracking().FirstOrDefaultAsync(x => x.PasswordResetToken == forgotPasswordRequest.Token && x.ResetTokenExpires > DateTime.Now);
			if (user != null)
			{
				if (user.SecurityCode == forgotPasswordRequest.Code)
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
				if (staff.SecurityCode == forgotPasswordRequest.Code)
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
				if (distributor.SecurityCode == forgotPasswordRequest.Code)
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

	public async Task<IActionResult> ChangeForgotPassword(NewPasswordRequest newPasswordRequest)
	{
		try
		{
			if (newPasswordRequest.Token.IsNullOrEmpty())
			{
				return new BadRequestObjectResult(new { error = "TOken is requried!!" });
			}
			var user = await db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.PasswordResetToken == newPasswordRequest.NewPassword && x.ResetTokenExpires>DateTime.Now);
			var staff = await db.StaffUsers.AsNoTracking().FirstOrDefaultAsync(x => x.PasswordResetToken == newPasswordRequest.NewPassword && x.ResetTokenExpires > DateTime.Now);
			var distributor = await db.Distributors.AsNoTracking().FirstOrDefaultAsync(x => x.PasswordResetToken == newPasswordRequest.NewPassword && x.ResetTokenExpires > DateTime.Now);
			var hashPassword = BCrypt.Net.BCrypt.HashPassword(newPasswordRequest.NewPassword);
			if (!BCrypt.Net.BCrypt.Verify(newPasswordRequest.ConfirmPassword, hashPassword)){
				return new BadRequestObjectResult(new { error = "Password and confirm password not match!!" });
			}
			if (user != null)
			{
				user.Password = hashPassword;
				user.ResetTokenExpires = null;
				user.PasswordResetToken = null;
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
				staff.ResetTokenExpires = null;
				staff.PasswordResetToken = null;
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
				distributor.ResetTokenExpires = null;
				distributor.PasswordResetToken = null;
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
	private string RandomToken()
	{
		return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
	}

	
}
