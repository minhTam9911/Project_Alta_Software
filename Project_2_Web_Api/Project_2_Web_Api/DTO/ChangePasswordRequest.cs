namespace Project_2_Web_Api.DTO;

public class ChangePasswordRequest
{
	public string? email { get; set; }
	public string? oldPassword {  get; set; }
	public string? newPassword { get; set; }
}
