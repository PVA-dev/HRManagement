namespace HRManagement.Models
{
	public class PersonalInfo : BaseEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? Patronymic { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string? Email { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
	}
}
