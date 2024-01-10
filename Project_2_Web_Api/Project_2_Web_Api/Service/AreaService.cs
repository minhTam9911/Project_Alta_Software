using Microsoft.AspNetCore.Mvc;
using Project_2_Web_Api.DTO;

namespace Project_2_Web_Api.Service;

public interface AreaService
{
	Task<IActionResult> Create(AreaDTO areaDTO);
	Task<IActionResult> Update(int id, string nameArea);
	Task<IActionResult> AddStaffToArea(int idArea, string idStaff);
	Task<IActionResult> RemoveStaffInArea(int idArea, int idStaff);
	Task<IActionResult> AddDistributorToArea(int idArea, int idStaff);
	Task<IActionResult> RemoveDistributorInArea(int idArea, int idStaff);
	Task<IActionResult> Delete(int id);
	Task<dynamic> FindAll();
	Task<dynamic> FindById(int id);
	Task<dynamic> FindByName(string name);
}
