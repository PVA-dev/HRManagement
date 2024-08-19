using HRManagement.Dto.PersonalInfoDtos;
using HRManagement.Models;
using Newtonsoft.Json;

namespace HRManagement.Dto.EmployeeDtos
{
    public class EmployeeDto : PersonalInfoDto
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
