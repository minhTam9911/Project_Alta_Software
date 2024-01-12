using Microsoft.AspNetCore.Mvc;
using Project_2_Web_Api.DTO;

namespace Project_2_Web_Api.Service;

public interface UserService 
{

	Task<IActionResult> Create(UserDTO userDTO);
	Task<IActionResult> Update(string id, UserDTO userDTO);
	Task<IActionResult> Delete(string id);
	Task<dynamic> FindAll();
	Task<dynamic> FindById(string id);
	Task<dynamic> FindByName(string name);
/*	Task<dynamic> FindByEmailOnly(string name);
	Task<dynamic> FindByEmails(string name);*/
	Task<IActionResult> ChangePassword(string id, string oldPassword, string newPassword);
	Task<IActionResult> ForgotPassword(string email);
	Task<IActionResult> VerifySecurityCode(string email, string code);
	Task<IActionResult> ChangeForgotPassword(string email, string newPassword);
	Task<IActionResult> SettingPermission(string id, int[] permissions);
}
