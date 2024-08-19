using HRManagement.Models;

namespace HRManagement.Data.Interfaces
{
	public interface IVacancyRepository : IRepository<Vacancy>
	{
		Task<List<Vacancy>> GetAllFullInfoAsync();
		Task<Vacancy?> GetFullInfoByIdAsync(int id);

    }
}
