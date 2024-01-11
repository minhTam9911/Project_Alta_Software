using Microsoft.AspNetCore.Mvc;
using Project_2_Web_Api.DTO;
using Project_2_Web_Api.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_2_Web_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StaffUserController : ControllerBase
{
/*	private readonly UserServiceAccessor _userServiceAccessor;*/
	private readonly StaffUserService userService;
	public StaffUserController(UserServiceAccessor userServiceAccessor, StaffUserService userService)
	{
	//	_userServiceAccessor = userServiceAccessor;
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
	[HttpPost("create")]
	public async Task<IActionResult> Create([FromBody] StaffUserDTO request)
	{
		return await userService.Create(request);
	}

	// PUT api/<UserController>/5
	[HttpPut("{id}")]
	public void Put(int id, [FromBody] string value)
	{
	}

	// DELETE api/<UserController>/5
	[HttpDelete("{id}")]
	public void Delete(int id)
	{
	}
}
