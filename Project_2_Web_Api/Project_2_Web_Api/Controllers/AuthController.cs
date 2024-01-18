using Microsoft.AspNetCore.Mvc;
using Project_2_Web_Api.DTO;
using Project_2_Web_Api.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_2_Web_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private AuthService authService;
	public AuthController(AuthService authService)
	{
		this.authService = authService;
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] UserAccessorDTO request)
	{
		return await authService.Login(request);
	}

}
