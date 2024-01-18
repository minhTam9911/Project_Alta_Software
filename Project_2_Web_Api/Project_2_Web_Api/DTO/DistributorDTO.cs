using System.ComponentModel.DataAnnotations;

namespace Project_2_Web_Api.DTO;

public class DistributorDTO
{
	[Required]
	public string? Name { get; set; }
	[Required]
	public string? Email { get; set; }
	[Required]
	public string? PhoneNumber { get; set; }
	public string? Address { get; set; }
	[Required]
	public string? SaleManagementId { get; set; }
	public string? SalesId { get; set; }
	public bool? IsStatus { get; set; }
	[Required]
	public int? PositionId { get; set; }
}
