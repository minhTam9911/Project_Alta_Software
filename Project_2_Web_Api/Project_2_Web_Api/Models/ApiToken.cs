using System.ComponentModel.DataAnnotations;

namespace Project_2_Web_Api.Models;

public class ApiToken
{
	[Key]
	public Guid UserId { get; set; }
	public string AccessToken { get; set; }
	public string RefreshToken { get; set; }
	public bool? IsActive { get; set; }
	
}
