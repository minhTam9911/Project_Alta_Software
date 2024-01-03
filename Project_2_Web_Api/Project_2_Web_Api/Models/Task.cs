using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_2_Web_API.Models;

public class Task
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	[Required]
	public string? Title { get; set; }
	[Required]
	public  Guid? AssignedStaffUserId {  get; set; }
	[Required]
	public  Guid? ReportingStaffUserId {  get; set; }
	[Required]
	public string? Description { get; set; }
	[Required]
	public DateTime? StartDate { get; set; }
	[Required]
	public DateTime? EndDate {  get; set; }

	public int? CreateBy { get; set; }
	public string? PhotoPath {  get; set; }
	public string? Status { get; set; }

	public virtual List<Comment>? Comments { get; set; }

	[ForeignKey(nameof(AssignedStaffUserId))]
	public virtual StaffUser? StaffUserAssignee { get; set; }
	[ForeignKey(nameof(ReportingStaffUserId))]
	public virtual StaffUser? StaffUserReposter { get; set; }
	
}
