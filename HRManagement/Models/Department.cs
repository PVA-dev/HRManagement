using System.ComponentModel.DataAnnotations.Schema;

namespace HRManagement.Models
{
	public class Department : BaseEntity
	{
		public string Name { get; set; }
		public Department? ParentDepartment { get; set; }
		[ForeignKey("Department")]
		public int? ParentDepartmentId { get; set; }
		public string? Description { get; set; }
	}
}
