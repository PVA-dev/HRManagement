using HRManagement.Models;

namespace HRManagement.Data.Interfaces
{
	public interface IEmployeeRepository : IRepository<Employee>
	{
		Task<Employee?> GetByPassportInfoAsync(string passportSeries, string passportNumber);
		Task<Employee?> GetIncludePersonalInfoByIdAsync(int id);
		Task<List<Employee>> GetAllEmployeesIncludePersonalInfoAsync();
		Task<bool> DeleteWithPersonalInfoAsync(Employee employee);
		Task<bool> HRManagerExistsAsync(int id);
	}
}
