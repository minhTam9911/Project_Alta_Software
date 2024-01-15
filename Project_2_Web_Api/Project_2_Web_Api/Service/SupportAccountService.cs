using Microsoft.AspNetCore.Mvc;

namespace Project_2_Web_Api.Service;

public interface SupportAccountService
{
	Task<IActionResult> ChangePassword(string id, string oldPassword, string newPassword);
	Task<IActionResult> ForgotPassword(string email);
	Task<IActionResult> VerifySecurityCode(string email, string code);
	Task<IActionResult> ChangeForgotPassword(string email, string newPassword);
	
}
