using HRManagement.Models;
using Newtonsoft.Json;

namespace HRManagement.Dto
{
	public class EmployeeDto
	{
		[JsonProperty("departmentId")]
		public int DepartmentId { get; set; }
		[JsonProperty("chiefId")]
		public int? ChiefId { get; set; }
		[JsonProperty("dateStartWork")]
		public DateTime DateStartWork { get; set; }
		[JsonProperty("positionId")]
		public int PositionId { get; set; }
		[JsonProperty("salary")]
		public int Salary { get; set; }
		[JsonProperty("firstName")]
		public string FirstName { get; set; }
		[JsonProperty("lastName")]
		public string LastName { get; set; }
		[JsonProperty("patronymic")]
		public string? Patronymic { get; set; }
		[JsonProperty("dateOfBirth")]
		public DateTime DateOfBirth { get; set; }
		[JsonProperty("email")]
		public string? Email { get; set; }
		[JsonProperty("phone")]
		public string Phone { get; set; }
		[JsonProperty("address")]
		public string Address { get; set; }
		[JsonProperty("passportSeries")]
		public string PassportSeries { get; set; }
		[JsonProperty("passportNumber")]
		public string PassportNumber { get; set; }

		public void FillFromModel(Employee employee)
		{
			ChiefId = employee.ChiefId;
			DepartmentId = employee.DepartmentId;
			DateStartWork = employee.DateStartWork;
			PositionId = employee.PositionId;
			Salary = employee.Salary;
			Address = employee.PersonalInfo.Address;
			DateOfBirth = employee.PersonalInfo.DateOfBirth;
			Email = employee.PersonalInfo.Email;
			FirstName = employee.PersonalInfo.FirstName;
			LastName = employee.PersonalInfo.LastName;
			PassportNumber = employee.PersonalInfo.PassportNumber;
			PassportSeries = employee.PersonalInfo.PassportSeries;
			Patronymic = employee.PersonalInfo.Patronymic;
			Phone = employee.PersonalInfo.Phone;
		}
	}
}
