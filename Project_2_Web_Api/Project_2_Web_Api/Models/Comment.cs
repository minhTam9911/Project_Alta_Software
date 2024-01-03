using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace Project_2_Web_API.Models;

public class Comment
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public int TaskId { get; set; }

	public Guid? AccountId { get; set; }

	public int? ParentCommentId { get; set; }

	public string Comment1 { get; set; } = null!;

	public DateTime CreateDate { get; set; }

	[ForeignKey("AccountId")]
	public virtual User Account { get; set; } = null!;

	public virtual Task Task { get; set; } = null!;
}
