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
	[HttpPut("setting-permission")]
	public async Task<IActionResult> SettingPermission([FromBody] SettingPermissionRequest request)
	{
		try
		{
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
		return await userService.ResetPassword(request.Id);
	}

}
