using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_2_Web_Api.DTO;
using Project_2_Web_Api.Service;
using System.Xml.Linq;

namespace Project_2_Web_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
	private readonly PositionGroupService positionGroupService;
	private readonly UserServiceAccessor userServiceAccessor;
	private readonly PositionService positionService;
	private readonly StaffUserService staffUserService;
	public TestController(PositionGroupService positionGroupService, UserServiceAccessor userServiceAccessor, PositionService positionService, StaffUserService staffUserService)
	{
		this.positionGroupService = positionGroupService;
		this.userServiceAccessor = userServiceAccessor;
		this.positionService = positionService;
		this.staffUserService = staffUserService;
	}
	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("test0")]
	public async Task<IActionResult> Test()
	{
		var data = new PositionGroupDTO { Name = "System" };
		return await positionGroupService.Create(data);

	}
	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("test1")]
	public async Task<IActionResult> Test1()
	{
		var data = new PositionDTO { Name = "Administrator", PositionGroupId = 1 };
		return await positionService.Create(data);

	}

	[Produces("application/json")]
	[Consumes("application/json")]
	[HttpGet("test2/{email}")]
	public async Task<IActionResult> Test2(string email)
	{
	var data = new StaffUserDTO { Fullname = "Supper admin", Email = email, PositionId = 1,IsStatus = false , StaffSuperiorId = null, StaffInteriorId = null};
	return await staffUserService.Create(data);
	
	}

}
