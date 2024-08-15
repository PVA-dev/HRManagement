using System.ComponentModel.DataAnnotations.Schema;

namespace HRManagement.Models
{
	public class Vacancy : BaseEntity
	{
		public Position Position { get; set; }
		[ForeignKey("Position")]
		public int PositionId { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateClosed { get; set; }
		public Employee? HRManager { get; set; }
		[ForeignKey("Employee")]
		public int? HRManagerId { get; set; }
		public Department Department { get; set; }
		[ForeignKey("Department")]
		public int DepartmentId { get; set; }
		public VacancyStatus Status { get; set; }
		[ForeignKey("VacancyStatus")]
		public int StatusId { get; set; }
	}
}