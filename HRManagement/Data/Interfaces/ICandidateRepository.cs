using HRManagement.Models;

namespace HRManagement.Data.Interfaces
{
    public interface ICandidateRepository : IRepository<Candidate>
    {
        Task<Candidate?> GetByPassportInfoAsync(string passportSeries, string passportNumber);
        Task<List<Candidate>> GetAllIncludeFullInfoAsync();
        Task<Candidate?> GetIncludeFullInfoByIdAsync(int id);
        Task<bool> DeleteWithPersonalInfoAsync(Candidate candidate);
    }
}
