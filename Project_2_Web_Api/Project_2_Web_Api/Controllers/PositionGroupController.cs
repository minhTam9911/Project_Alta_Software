using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_2_Web_Api.Service;
using Project_2_Web_API.Models;



namespace Project_2_Web_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PositionGroupController : ControllerBase
{

	private readonly PositionGroupService positionGroupService;
	public PositionGroupController(PositionGroupService positionGroupService)
	{
		this.positionGroupService = positionGroupService;
	}


	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("find-all")]
	public  async Task<IActionResult> Get()
	{
		try
		{
			return  Ok( await positionGroupService.FindAll());
		}
		catch(Exception ex)
		{
			return BadRequest(new { msg = ex.Message });
		}
		
	}


	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("find-by-id/{id}")]
	public async Task<IActionResult> GetById(string id)
	{
		try
		{
			return Ok(await positionGroupService.FindById(id));
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
			return Ok(await positionGroupService.FindByName(name));
		}
		catch (Exception ex)
		{
			return BadRequest(new { msg = ex.Message });
		}
	}

	
	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPost("create")]
	public async Task<IActionResult> Create([FromBody]PositionGroup value)
	{
		return await positionGroupService.Create(value);
	}
	

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("update/{id}")]
	public async Task<IActionResult> Put(string id, [FromBody] PositionGroup value)
	{
		return await positionGroupService.Update(id, value);
	}

	

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpDelete("delete/{id}")]
	public async Task<IActionResult> Delete(string id)
	{
		return await positionGroupService.Delete(id);
	}
}
