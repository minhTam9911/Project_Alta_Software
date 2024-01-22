using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Project_2_Web_Api.DTO;
using Project_2_Web_API.Models;
using System.Security.Claims;

namespace Project_2_Web_Api.Service.Impl;

public class TaskForVisitServiceImpl : TaskForVisitService
{

	private DatabaseContext db;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IMapper mapper;
	private IConfiguration configuration;
	private UserServiceAccessor userServiceAccessor;
	public TaskForVisitServiceImpl(DatabaseContext db, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserServiceAccessor userServiceAccessor)
	{
		this.mapper = mapper;
		this.configuration = configuration;
		_httpContextAccessor = httpContextAccessor;
		this.db = db;
		this.userServiceAccessor = userServiceAccessor;
	}

	public  async Task<IActionResult> Create(TaskForVisitDTO taskForVisitDTO)
	{
		var modelState = _httpContextAccessor.HttpContext?.Items["MS_ModelState"] as ModelStateDictionary;
		var taskForVisit = mapper.Map<TaskForVisit>(taskForVisitDTO);
		try
		{
			if (modelState != null && !modelState.IsValid)
			{
				return new BadRequestObjectResult(modelState);
			}
			else
			{
				taskForVisit.CreateBy = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name));
				taskForVisit.Status = "Đã Giao Việc";
				db.TaskForVisit.Add(taskForVisit);
				if(await db.SaveChangesAsync() > 0)
				{
					return new OkObjectResult(new { msg = true });
				}
				else
				{
					return new BadRequestObjectResult(new { error = false });
				}
			}
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new { error = ex.Message });
		}
	}

	public async Task<IActionResult> DetailTask(int id)
	{
		try
		{
			if (await db.TaskForVisit.AnyAsync() == false)
			{
				return new OkObjectResult(new { error = "Data is null !" });
			}
			else
			{
				if (await userServiceAccessor.IsGuest())
				{
					return new UnauthorizedResult();
				}
				else if (await userServiceAccessor.IsDistributor())
				{
					return new UnauthorizedResult();
				}
				else if (await userServiceAccessor.IsSales())
				{
					return new OkObjectResult(
					await db.TaskForVisit.Where(x => x.Id == id).Select(x => new
					{
						id = x.Id,
						title = x.Title,
						assignedStaffUser = new
						{
							id = x.AssignedStaffUserId,
							name = x.StaffUserAssignee.Fullname
						},
						reportStaffUser = new
						{
							id = x.ReportingStaffUserId,
							name = x.StaffUserReposter.Fullname
						},
						description = x.Description,
						startDate = x.StartDate,
						endDate = x.EndDate,

					}).ToListAsync()
					);
				}
				else;
				return new OkObjectResult(
					await db.TaskForVisit.Where(x => x.Id == id).Select(x => new
					{
						id = x.Id,
						title = x.Title,
						assignedStaffUser = new
						{
							id = x.AssignedStaffUserId,
							name = x.StaffUserAssignee.Fullname
						},
						reportStaffUser = new
						{
							id = x.ReportingStaffUserId,
							name = x.StaffUserReposter.Fullname
						},
						description = x.Description,
						startDate = x.StartDate,
						endDate = x.EndDate,

					}).ToListAsync()
					);
			}
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new
			{
				error = ex.Message
			});
		}
	}

	public async Task<IActionResult> FindAll()
	{
		try
		{
			if (await db.TaskForVisit.AnyAsync() == false)
			{
				return new OkObjectResult(new { error = "Data is null !" });
			}
			else
			{
				if (await userServiceAccessor.IsGuest())
				{
					return new UnauthorizedResult();
				}
				else if (await userServiceAccessor.IsDistributor())
				{
					return new UnauthorizedResult();
				}
				else if (await userServiceAccessor.IsSales())
				{
					return new OkObjectResult(
					await db.TaskForVisit.Where(x => x.AssignedStaffUserId == Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name))).Select(x => new
					{
						id = x.Id,
						title = x.Title,
						assignedStaffUser = new
						{
							id = x.AssignedStaffUserId,
							name = x.StaffUserAssignee.Fullname
						},
						reportStaffUser = new
						{
							id = x.ReportingStaffUserId,
							name = x.StaffUserReposter.Fullname
						},
						description = x.Description,
						startDate = x.StartDate,
						endDate = x.EndDate,

					}).ToListAsync()
					);
				}
				else;
				return new OkObjectResult(
					await db.TaskForVisit.Where(x => x.AssignedStaffUserId == Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name))).Select(x => new
					{
						id = x.Id,
						title = x.Title,
						assignedStaffUser = new
						{
							id = x.AssignedStaffUserId,
							name = x.StaffUserAssignee.Fullname
						},
						reportStaffUser = new
						{
							id = x.ReportingStaffUserId,
							name = x.StaffUserReposter.Fullname
						},
						description = x.Description,
						startDate = x.StartDate,
						endDate = x.EndDate,

					}).ToListAsync()
					);
			}
		}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new
			{
				error = ex.Message
			});
		}
	}

	public Task<IActionResult> FindById(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<IActionResult> TaskForMe()
	{
		try
		{
			if (await db.TaskForVisit.AnyAsync() == false)
			{
			return new OkObjectResult(new { error = "Data is null !" });
			}
			else
			{
			if (await userServiceAccessor.IsGuest())
			{
				return new UnauthorizedResult();
			}
			else if (await userServiceAccessor.IsDistributor())
			{
					return new UnauthorizedResult();
			}
			else if (await userServiceAccessor.IsSales())
			{
				return new OkObjectResult(
				await db.TaskForVisit.Where(x => x.AssignedStaffUserId == Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name))).Select(x => new
				{
					id = x.Id,
					title = x.Title,
					assignedStaffUser = new
					{
						id = x.AssignedStaffUserId,
						name = x.StaffUserAssignee.Fullname
					},
					reportStaffUser = new
					{
						id = x.ReportingStaffUserId,
						name = x.StaffUserReposter.Fullname
					},
					description = x.Description,
					startDate = x.StartDate,
					endDate = x.EndDate,

				}).ToListAsync()
				);
			}
			else;
				return new OkObjectResult(
					await db.TaskForVisit.Where(x => x.AssignedStaffUserId == Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name))).Select(x => new
					{
						id = x.Id,
						title = x.Title,
						assignedStaffUser = new
						{
							id = x.AssignedStaffUserId,
							name = x.StaffUserAssignee.Fullname
						},
						reportStaffUser = new
						{
							id = x.ReportingStaffUserId,
							name = x.StaffUserReposter.Fullname
						},
						description = x.Description,
						startDate = x.StartDate,
						endDate = x.EndDate,

					}).ToListAsync()
					);
			}
	}
		catch (Exception ex)
		{
			return new BadRequestObjectResult(new
			{
				error = ex.Message
});
		}
	}

	public Task<IActionResult> UploadFile(IFormFile fileAssigned, IFormFile fileReport)
	{
		throw new NotImplementedException();
	}

	public Task<IActionResult> UploadFileTaskForMe(int id, IFormFile fileReport, string status)
	{
		throw new NotImplementedException();
	}
}
