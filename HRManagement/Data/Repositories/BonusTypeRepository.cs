using HRManagement.Data.Interfaces;
using HRManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Data.Repositories
{
    public class BonusTypeRepository : Repository<BonusType>, IBonusTypeRepository
    {
        public BonusTypeRepository(AppDbContext dbContext) : base(dbContext){}

        public Task<BonusType?> GetByNameAsync(string name)
        {
            return _dbContext.BonusTypes.FirstOrDefaultAsync(b => b.Name == name);
        }
    }
}
