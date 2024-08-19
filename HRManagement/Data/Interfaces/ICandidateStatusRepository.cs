using HRManagement.Models;

namespace HRManagement.Data.Interfaces
{
    public interface ICandidateStatusRepository : IRepository<CandidateStatus>
    {
        Task<CandidateStatus?> GetByNameAsync(string name);
    }
}
