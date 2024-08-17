using HRManagement.Models;

namespace HRManagement.Data.Interfaces
{
	public interface IEmployeeRepository : IRepository<Employee>
	{
		Task<Employee?> GetByPassportInfoAsync(string passportSeries, string passportNumber);
		Task<Employee?> GetIncludePersonalInfoById(int id);
		Task<List<Employee>> GetAllEmployeesIncludePersonalInfo();
		Task<bool> DeleteWithPersonalInfoAsync(Employee employee);
	}
}
