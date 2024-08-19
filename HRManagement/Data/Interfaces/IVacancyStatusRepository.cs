using HRManagement.Models;

namespace HRManagement.Data.Interfaces
{
    public interface IVacancyStatusRepository : IRepository<VacancyStatus>
    {
        Task<VacancyStatus?> GetByNameAsync(string name);
    }
}
