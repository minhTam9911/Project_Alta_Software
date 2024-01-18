using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project_2_Web_Api.DTO;
using Project_2_Web_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http.ModelBinding;

namespace Project_2_Web_Api.Service.Impl;

public class AuthServiceImpl : AuthService
{

	private DatabaseContext db;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper mapper;
	private IConfiguration configuration;
	public AuthServiceImpl(DatabaseContext db, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper)
	{
		this.mapper = mapper;
		this.configuration = configuration;
		_httpContextAccessor = httpContextAccessor;
		this.db = db;
	}


	public async Task<IActionResult> Login(UserAccessorDTO userAccessorDTO)
	{
		var modelState = _httpContextAccessor.HttpContext?.Items["MS_ModelState"] as ModelStateDictionary;
		try
		{
			if (modelState != null && !modelState.IsValid)
			{
				return new BadRequestObjectResult(modelState);
			}
			else
			{
				if(await db.Users.FirstOrDefaultAsync(x=>x.Email == userAccessorDTO.Username) != null)
				{
					var data = await db.Users.FirstOrDefaultAsync(x => x.Email == userAccessorDTO.Username);
					if (BCrypt.Net.BCrypt.Verify(userAccessorDTO.Password, data.Password)){
						return new OkResult();
					}
					else
					{
						return new BadRequestObjectResult(new {error = "Passwords do not match" });
					}
				}
				else if (await db.StaffUsers.FirstOrDefaultAsync(x => x.Email == userAccessorDTO.Username) != null)
				{
					var data = await db.StaffUsers.FirstOrDefaultAsync(x => x.Email == userAccessorDTO.Username);
					if (BCrypt.Net.BCrypt.Verify(userAccessorDTO.Password, data.Password))
					{
						return new OkResult();
					}
					else
					{
						return new BadRequestObjectResult(new { error = "Passwords do not match" });
					}
				}
				else if (await db.Distributors.FirstOrDefaultAsync(x => x.Email == userAccessorDTO.Username) != null)
				{
					var data = await db.Distributors.FirstOrDefaultAsync(x => x.Email == userAccessorDTO.Username);
					if (BCrypt.Net.BCrypt.Verify(userAccessorDTO.Password, data.Password))
					{
						return new OkResult();
					}
					else
					{
						return new BadRequestObjectResult(new { error = "Passwords do not match" });
					}
				}
				else
				{
					return new BadRequestObjectResult(new { error = "Username does not exist" });
				}
			}
		}
		catch(Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}
}
