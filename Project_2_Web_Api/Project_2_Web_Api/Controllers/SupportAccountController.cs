using Microsoft.AspNetCore.Mvc;
using Project_2_Web_Api.DTO;
using Project_2_Web_Api.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_2_Web_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SupportAccountController : ControllerBase
{
	private readonly SupportAccountService supportAccountService;
	public SupportAccountController(SupportAccountService supportAccountService)
	{
		this.supportAccountService = supportAccountService;
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("change-password")]
	public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
	{
		string? id = User?.Identity.Name;
		return await supportAccountService.ChangePassword(id, request.oldPassword, request.newPassword);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("forgot-password")]
	public async Task<IActionResult> ForgotPassword([FromBody] VerifyCode request)
	{
		return await supportAccountService.ForgotPassword(request.email);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("verify-code")]
	public async Task<IActionResult> VerifyCode([FromBody] VerifyCode request)
	{
		return await supportAccountService.VerifySecurityCode(request.email, request.code);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("change-forgot-password")]
	public async Task<IActionResult> ChangeForgotPassword([FromBody] ChangePasswordRequest request)
	{
		return await supportAccountService.ChangeForgotPassword(request.email, request.newPassword);
	}

	
}
