using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_2_Web_Api.DTO;
using Project_2_Web_Api.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_2_Web_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StaffUserController : ControllerBase
{
	private readonly UserServiceAccessor userServiceAccessor;
	private readonly StaffUserService staffUserService;
	public StaffUserController(UserServiceAccessor userServiceAccessor, StaffUserService staffUserService)
	{
		this.userServiceAccessor = userServiceAccessor;
		this.staffUserService = staffUserService;
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("find-all")]
	public async Task<IActionResult> GetAll()
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
			return Ok(await staffUserService.FindAll());
		}
		catch (Exception ex)
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
			return Ok(await staffUserService.FindById(id));
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
			return Ok(await staffUserService.FindByName(name));
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPost("create")]
	public async Task<IActionResult> Create([FromBody] StaffUserDTO request)
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
		return await staffUserService.Create(request);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("update/{id}")]
	public async Task<IActionResult> Update(string id, [FromBody] StaffUserDTO request)
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
		return await staffUserService.Update(id, request);
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
		if(await userServiceAccessor.IsSales())
		{
			return Unauthorized();
		}
		if (await userServiceAccessor.IsSales())
		{
			return Unauthorized();
		}
		return await staffUserService.Delete(id);
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
		return await staffUserService.ResetPassword(request.Id);
	}
	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("get-me")]
	public async Task<IActionResult> GetMe()
	{
		try
		{
			return Ok(await userServiceAccessor.GetByMe());
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}
}
