using HRManagement.Data.Interfaces;
using HRManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Data.Repositories
{
	public class VacancyRepository : Repository<Vacancy>, IVacancyRepository
	{
		public VacancyRepository(AppDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<List<Vacancy>> GetAllFullInfoAsync()
		{
			return await _dbContext.Vacancies
									.Include(x => x.Status)
									.Include(x => x.HRManager).ThenInclude(c => c.PersonalInfo)
									.Include(x => x.Position)
									.Include(x => x.Department)
									.ToListAsync();
		}

        public async Task<Vacancy?> GetFullInfoByIdAsync(int id)
        {
            return await _dbContext.Vacancies
                                    .Include(x => x.Status)
                                    .Include(x => x.HRManager).ThenInclude(c => c.PersonalInfo)
                                    .Include(x => x.Position)
                                    .Include(x => x.Department)
                                    .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
