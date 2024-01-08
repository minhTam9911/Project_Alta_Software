using Microsoft.AspNetCore.Mvc;
using Project_2_Web_API.Models;

namespace Project_2_Web_Api.Service;

public interface PositionGroupService
{

	Task<IActionResult> Create(PositionGroup positionGroup);
	Task<IActionResult> Update(string id,PositionGroup positionGroup);
	Task<IActionResult> Delete(string id);
	Task<dynamic> FindAll();
	Task<dynamic> FindById(string id);
	Task<dynamic> FindByName(string name);

}
