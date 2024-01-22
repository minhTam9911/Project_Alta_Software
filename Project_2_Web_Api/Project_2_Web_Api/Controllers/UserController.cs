using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_2_Web_Api.DTO;
using Project_2_Web_Api.Service;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_2_Web_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
	private readonly UserService userService;
	private readonly UserServiceAccessor userServiceAccessor;
	public UserController(UserService userService, UserServiceAccessor userServiceAccessor)
	{
		this.userService = userService;
		this.userServiceAccessor = userServiceAccessor;
	}
	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("find-all")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			if(await userServiceAccessor.IsGuest())
			{
				return Unauthorized();
			}if(await userServiceAccessor.IsDistributor())
			{
				return Unauthorized();
			}
			if (await userServiceAccessor.IsSales())
			{
				return Unauthorized();
			}
			return Ok(await userService.FindAll());
		}catch(Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("find-by-id/{id}")]
	public async Task<IActionResult> GetById(string id)
	{
		try
		{
			if (await userServiceAccessor.IsGuest())
			{
				return Unauthorized();
			}
			if (await userServiceAccessor.IsDistributor())
			{
				return Unauthorized();
			}
			if (await userServiceAccessor.IsSales())
			{
				return Unauthorized();
			}
			return Ok(await userService.FindById(id));
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("find-by-name/{name}")]
	public async Task<IActionResult> GetByName(string name)
	{
		try
		{
			if (await userServiceAccessor.IsGuest())
			{
				return Unauthorized();
			}
			if (await userServiceAccessor.IsDistributor())
			{
				return Unauthorized();
			}
			if (await userServiceAccessor.IsSales())
			{
				return Unauthorized();
			}
			return Ok(await userService.FindByName(name));
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPost("create")]
	public async Task<IActionResult> Create([FromBody] UserDTO request)
	{
		if (await userServiceAccessor.IsGuest())
		{
			return Unauthorized();
		}
		if (await userServiceAccessor.IsDistributor())
		{
			return Unauthorized();
		}
		if (await userServiceAccessor.IsSales())
		{
			return Unauthorized();
		}
		return await userService.Create(request);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("update/{id}")]
	public async Task<IActionResult> Update(string id, [FromBody] UserDTO request)
	{
		if (await userServiceAccessor.IsGuest())
		{
			return Unauthorized();
		}
		if (await userServiceAccessor.IsDistributor())
		{
			return Unauthorized();
		}
		if (await userServiceAccessor.IsSales())
		{
			return Unauthorized();
		}
		return await userService.Update(id, request);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpDelete("delete/{id}")]
	public async Task<IActionResult> Delete(string id)
	{
		if (await userServiceAccessor.IsGuest())
		{
			return Unauthorized();
		}
		if (await userServiceAccessor.IsDistributor())
		{
			return Unauthorized();
		}
		if (await userServiceAccessor.IsSales())
		{
			return Unauthorized();
		}
		return await userService.Delete(id);
	}
	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("setting-permission")]
	public async Task<IActionResult> SettingPermission([FromBody] SettingPermissionRequest request)
	{
		try
		{
			if (await userServiceAccessor.IsGuest())
			{
				return Unauthorized();
			}
			if (await userServiceAccessor.IsDistributor())
			{
				return Unauthorized();
			}
			if (await userServiceAccessor.IsSales())
			{
				return Unauthorized();
			}
			return await userService.SettingPermission(request.Id, request.PermissionId);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}

	}
	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("reset-password")]
	public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
	{
		if (await userServiceAccessor.IsGuest())
		{
			return Unauthorized();
		}
		if (await userServiceAccessor.IsDistributor())
		{
			return Unauthorized();
		}
		if (await userServiceAccessor.IsSales())
		{
			return Unauthorized();
		}
		return await userService.ResetPassword(request.Id);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("get-me")]
	public async Task<IActionResult> GetMe()
	{
		try
		{
			if (await userServiceAccessor.IsDistributor())
			{
				return Unauthorized();
			}
			if (await userServiceAccessor.IsSales())
			{
				return Unauthorized();
			}
			if(await userServiceAccessor.IsSystem())
			{
				return Unauthorized();
			}
			return Ok(await userService.FindById(User?.Identity?.Name));
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}
}
