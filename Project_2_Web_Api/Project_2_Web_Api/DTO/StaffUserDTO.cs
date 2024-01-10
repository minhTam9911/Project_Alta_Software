using Project_2_Web_API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project_2_Web_Api.DTO;

public class StaffUserDTO
{
	public Guid Id { get; set; } = Guid.NewGuid();
	[Required]
	public string? Fullname { get; set; }
	[Required]
	[EmailAddress]
	public string? Email { get; set; }
	[Required]
	public string? Password { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Address { get; set; }
	[Required]
	public int? PositionId { get; set; }
}
