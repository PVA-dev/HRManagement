using HRManagement.Data.Interfaces;
using HRManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Data.Repositories
{
    public class CandidateStatusRepository : Repository<CandidateStatus>, ICandidateStatusRepository
    {
        public CandidateStatusRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<CandidateStatus?> GetByNameAsync(string name)
        {
            return await _dbContext.CandidateStatuses.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
