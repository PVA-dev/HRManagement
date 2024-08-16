using System.ComponentModel.DataAnnotations.Schema;

namespace HRManagement.Models
{
	public class Department : BaseEntity
	{
		public string Name { get; set; }
		public string? Description { get; set; }
	}
}
