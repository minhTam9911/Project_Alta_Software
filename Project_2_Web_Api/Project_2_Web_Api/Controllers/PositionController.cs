using Microsoft.AspNetCore.Mvc;
using Project_2_Web_Api.Service;
using Project_2_Web_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_2_Web_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PositionController : ControllerBase
{

	private readonly PositionService positionService;
	public PositionController(PositionService positionService)
	{
		this.positionService = positionService;
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("find-all")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			return Ok(await positionService.FindAll());
		}catch(Exception ex)
		{
			return BadRequest(new { msg = ex.Message });
		}
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("find-by-id/{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		try
		{
			return Ok(await positionService.FindById(id));
		}
		catch (Exception ex)
		{
			return BadRequest(new { msg = ex.Message });
		}
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("find-by-name/{name}")]
	public async Task<IActionResult> GetByName(string name)
	{
		try
		{
			return Ok(await positionService.FindByName(name));
		}
		catch (Exception ex)
		{
			return BadRequest(new { msg = ex.Message });
		}
	}


	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPost("create")]
	public async Task<IActionResult> Create([FromBody] Position position)
	{
		return await positionService.Create(position);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("update/{id}")]
	public async Task<IActionResult> Update(int id, [FromBody] Position position)
	{
		return await positionService.Update(id, position);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpDelete("delete/{id}")]
	public  async Task<IActionResult> Delete(int id)
	{
		return await positionService.Delete(id);
	}
}
