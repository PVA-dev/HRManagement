using HRManagement.Data.Interfaces;
using HRManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Data.Repositories
{
    public class VacancyStatusRepository : Repository<VacancyStatus>, IVacancyStatusRepository
    {
        public VacancyStatusRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<VacancyStatus?> GetByNameAsync(string name)
        {
            return await _dbContext.VacancyStatuses.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
