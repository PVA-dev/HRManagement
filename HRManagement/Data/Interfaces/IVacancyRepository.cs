using HRManagement.Models;

namespace HRManagement.Data.Interfaces
{
	public interface IVacancyRepository : IRepository<Vacancy>
	{
		Task<List<Vacancy>> GetAllVacanciesFullInfoAsync();
	}
}
