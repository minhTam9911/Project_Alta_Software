using Microsoft.AspNetCore.Mvc;
using Project_2_Web_Api.DTO;

namespace Project_2_Web_Api.Service;

public interface StaffUserService
{
	Task<dynamic> FindById(int id);
	Task<IActionResult> Create(StaffUserDTO staffUserDto);
	Task<IActionResult> Update(Guid id, StaffUserDTO staffUserDto);
	Task<IActionResult> Delete(string id);
	Task<dynamic> FindAll();

}
