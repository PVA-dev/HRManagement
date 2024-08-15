using System.ComponentModel.DataAnnotations.Schema;

namespace HRManagement.Models
{
	public class Bonus : BaseEntity
	{
		public Employee Employee { get; set; }
		[ForeignKey("Employee")]
		public int EmployeeId { get; set; }
		public DateTime DateCreated { get; set; }
		public BonusType BonusType { get; set; }
		[ForeignKey("BonusType")]
		public int BonusTypeId { get; set; }
	}
}
