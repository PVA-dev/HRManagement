using System.ComponentModel.DataAnnotations.Schema;

namespace HRManagement.Models
{
	public class Employee : BaseEntity
	{
		public PersonalInfo PersonalInfo { get; set; }
        public int PersonalInfoId { get; set; }
        public Department Department { get; set; }
		[ForeignKey("Department")]
		public int DepartmentId { get; set; }
		public Employee? Chief { get; set; }
		[ForeignKey("Employee")]
		public int? ChiefEmployeeId { get; set; }
		public DateTime DateStartWork { get; set; }
		public DateTime DateDismissal { get; set; }
		public Position Position { get; set; }
		public int PositionId { get; set; }
		public int Salary { get; set; }
	}
}