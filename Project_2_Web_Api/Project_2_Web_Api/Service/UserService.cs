using Microsoft.AspNetCore.Mvc;

namespace Project_2_Web_Api.Service;

public interface UserService
{
	Task<IActionResult> Create(string UserJson);
	Task<IActionResult> Delete(string id);
	Task<IActionResult> Update(string UserJson);
	Task<IActionResult> FindAll();

}
