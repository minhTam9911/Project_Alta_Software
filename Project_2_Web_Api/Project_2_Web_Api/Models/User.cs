using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_2_Web_API.Models;

public partial class User
{
	[Key,]
	public Guid Id { get; set; }
	[Required]
	public string? FullName { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Address { get; set; }
	[Required]	
	public string? Email { get; set; }
	[Required]
	public string? Password { get; set; }
	public bool? IsStatus{ get; set; }
	public string? SecurityCode { get; set; }
	public string TokenRefresh { get; set; } = null!;
	public DateTime? CreatedDateToken { get; set; }
	public DateTime? ExpireDateToken {  get; set; }	
	public string? CreateBy {  get; set; }
	[Required]
	public int? PositionId { get; set; }
	public DateTime CreatedDate {  get; set; }
	[ForeignKey("PositionId")]
	public virtual Position? Position { get; set; }
	public virtual ICollection<GrantPermission> GrantPermissions { get; set; } = new List<GrantPermission>();
}
