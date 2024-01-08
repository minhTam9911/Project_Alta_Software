using Microsoft.AspNetCore.Mvc;
using Project_2_Web_API.Models;

namespace Project_2_Web_Api.Service;

public interface PositionService
{
	Task<IActionResult> Create(Position position);
	Task<IActionResult> Update(int id, Position position);
	Task<IActionResult> Delete(int id);
	Task<dynamic> FindAll();
	Task<dynamic> FindById(string id);
	Task<dynamic> FindByName(string name);
}
