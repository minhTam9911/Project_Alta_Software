using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project_2_Web_Api.DTO;
using Project_2_Web_Api.Service;
using Project_2_Web_API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project_2_Web_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AreaController : ControllerBase
{
	private readonly AreaService areaService;

	public AreaController(AreaService areaService) {
		this.areaService = areaService; 
	}
	
	[HttpGet]
	public IEnumerable<string> Get()
	{
		return new string[] { "value1", "value2" };
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("find-all")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			return Ok(await areaService.FindAll());
		}catch(Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("find-by-id/{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		try
		{
			return Ok(await areaService.FindById(id));
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
			return Ok(await areaService.FindByName(name));
		}
		catch (Exception ex)
		{
			return BadRequest(new { error = ex.Message });
		}
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPost("create")]
	public async Task<IActionResult> Create([FromBody] AreaDTO request)
	{
		return await areaService.Create(request);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("add-staff-to-area/{idArea}")]
	public async Task<IActionResult> AddStaffToArea(int idArea, [FromQuery(Name = "id-staff")] string idStaff )
	{
		return await areaService.AddStaffToArea(idArea,idStaff);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpPut("update/{id}")]
	public async Task<IActionResult> Update(int id, [FromQuery(Name = "name-area")] string nameArea)
	{
		return await areaService.Update(id, nameArea);
	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpDelete("delete/{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		return await areaService.Delete(id);
	}
}
