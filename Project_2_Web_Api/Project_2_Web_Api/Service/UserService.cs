using Microsoft.AspNetCore.Mvc;

namespace Project_2_Web_Api.Service;

public interface UserService 
{
	Task<IActionResult> FindById(int id);
	Task<IActionResult> Create(string userJson);
	Task<IActionResult> Update(string userJson);
	Task<IActionResult> Delete(string id);
	Task<IActionResult> FindAll();

}
