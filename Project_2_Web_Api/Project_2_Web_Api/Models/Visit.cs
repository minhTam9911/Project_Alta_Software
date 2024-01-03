using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Project_2_Web_API.Models;

public partial class Visit
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	[Required]
	public DateTime? Calendar {  get; set; }
	[Required]
	public string? Time {  get; set; }
	[Required]
	public int? DistributorId { get; set; }
	[Required]
	public int? GuestOfVisit { get; set; }
	
	public int? CreateBy {  get; set; }
	[Required]
	public string? PurposeOfVisit {  get; set; }
	
	public string? Status { get; set; }	

	public virtual Distributor? Distributor { get; set; }
	public virtual StaffUser? StaffUser { get; set; }

}
