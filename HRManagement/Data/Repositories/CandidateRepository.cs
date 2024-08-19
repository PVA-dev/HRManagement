using HRManagement.Data.Interfaces;
using HRManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Data.Repositories
{
    public class CandidateRepository : Repository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<List<Candidate>> GetAllIncludeFullInfoAsync()
        {
            return await _dbContext.Candidates.Include(c => c.Vacancy)
                                                .Include(c => c.PersonalInfo)
                                                .Include(c => c.Status)
                                                .ToListAsync();
        }

        public async Task<Candidate?> GetIncludeFullInfoByIdAsync(int id)
        {
            return await _dbContext.Candidates.Include(c => c.Vacancy)
                                                .Include(c => c.PersonalInfo)
                                                .Include(c => c.Status)
                                                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Candidate?> GetByPassportInfoAsync(string passportSeries, string passportNumber)
        {
            return await _dbContext.Candidates.Include(pi => pi.PersonalInfo)
                                              .FirstOrDefaultAsync(x => x.PersonalInfo.PassportNumber == passportNumber
                                                                        && x.PersonalInfo.PassportSeries == passportSeries);
        }

        public async Task<bool> DeleteWithPersonalInfoAsync(Candidate candidate)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _dbContext.Candidates.Remove(candidate);

                    var personalInfo = await _dbContext.PersonalInfos.FirstOrDefaultAsync(x => x.Id == candidate.PersonalInfoId);
                    if (personalInfo != null)
                    {
                        _dbContext.PersonalInfos.Remove(personalInfo);
                    }
                    await SaveAsync();

                    await transaction.CommitAsync();
                 }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                return true;
            }
        }
    }
}
