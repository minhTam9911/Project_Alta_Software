using Microsoft.AspNetCore.Mvc;
using Project_2_Web_Api.DTO;
using Project_2_Web_Api.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_2_Web_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly UserService userService;
	public UserController(UserService userService)
	{
		this.userService = userService;
	}
	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("find-all")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
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
		return await userService.Create(request);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("update/{id}")]
	public async Task<IActionResult> Update(string id, [FromBody] UserDTO request)
	{
		return await userService.Update(id, request);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpDelete("delete/{id}")]
	public async Task<IActionResult> Delete(string id)
	{
		return await userService.Delete(id);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("change-password")]
	public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
	{
		string? id = User?.Identity.Name;
		return await userService.ChangePassword(id, request.oldPassword,request.newPassword);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("forgot-password")]
	public async Task<IActionResult> ForgotPassword([FromBody]VerifyCode request)
	{
		return await userService.ForgotPassword(request.email);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("verify-code")]
	public async Task<IActionResult> VerifyCode([FromBody] VerifyCode request)
	{
		return await userService.VerifySecurityCode(request.email,request.code);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("change-forgot-password")]
	public async Task<IActionResult> ChangeForgotPassword([FromBody] ChangePasswordRequest request)
	{
		return await userService.ChangeForgotPassword(request.email, request.newPassword);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("setting-permission")]
	public async Task<IActionResult> SettingPermission([FromBody] SettingPermissionRequest request)
	{
		try
		{
				return await userService.SettingPermission(request.Id, request.PermissionId);
		}
		catch(Exception ex)
		{
			return BadRequest(ex.Message);
		}
		
	}

}
