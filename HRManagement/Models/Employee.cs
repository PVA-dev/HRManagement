using HRManagement.Dto.EmployeeDtos;
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
		public int? ChiefId { get; set; }
		public DateTime DateStartWork { get; set; }
		public DateTime? DateDismissal { get; set; }
		public Position Position { get; set; }
		[ForeignKey("Position")]
		public int PositionId { get; set; }
		public int Salary { get; set; }

		public void FillFromDto(EmployeeDto employeeDto)
		{
			DateStartWork = employeeDto.DateStartWork;
			Salary = employeeDto.Salary;
			PositionId = employeeDto.PositionId;
			DepartmentId = employeeDto.DepartmentId;
			ChiefId = employeeDto.ChiefId;

			if (PersonalInfo == null)
			{
				PersonalInfo = new PersonalInfo();
			}

			PersonalInfo.FirstName = employeeDto.FirstName;
			PersonalInfo.LastName = employeeDto.LastName;
			PersonalInfo.Patronymic = employeeDto.Patronymic;
			PersonalInfo.PassportNumber = employeeDto.PassportNumber;
			PersonalInfo.PassportSeries = employeeDto.PassportSeries;
			PersonalInfo.Phone = employeeDto.Phone;
			PersonalInfo.Email = employeeDto.Email;
			PersonalInfo.Address = employeeDto.Address;
			PersonalInfo.DateOfBirth = employeeDto.DateOfBirth;
		}
	}
}