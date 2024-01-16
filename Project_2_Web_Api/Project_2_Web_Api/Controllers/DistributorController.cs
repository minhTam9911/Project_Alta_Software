using Microsoft.AspNetCore.Mvc;
using Project_2_Web_Api.DTO;
using Project_2_Web_Api.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_2_Web_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DistributorController : ControllerBase
{
	private readonly DistributorService distributorService;
	public DistributorController(DistributorService distributorService)
	{
		this.distributorService = distributorService;
	}
	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("find-all")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			return Ok(await distributorService.FindAll());
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
			return Ok(await distributorService.FindById(id));
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
			return Ok(await distributorService.FindByName(name));
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPost("create")]
	public async Task<IActionResult> Create([FromBody] DistributorDTO request)
	{
		return await distributorService.Create(request);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("update/{id}")]
	public async Task<IActionResult> Update(string id, [FromBody] DistributorDTO request)
	{
		return await distributorService.Update(id, request);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpDelete("delete/{id}")]
	public async Task<IActionResult> Delete(string id)
	{
		return await distributorService.Delete(id);
	}
	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("setting-permission")]
	public async Task<IActionResult> SettingPermission([FromBody] SettingPermissionRequest request)
	{
		try
		{
			return await distributorService.SettingPermission(request.Id, request.PermissionId);
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
		return await distributorService.ResetPassword(request.Id);
	}
}
