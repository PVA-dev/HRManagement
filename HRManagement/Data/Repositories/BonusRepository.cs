using HRManagement.Data.Interfaces;
using HRManagement.Models;

namespace HRManagement.Data.Repositories
{
    public class BonusRepository : Repository<Bonus>, IBonusRepository
    {
        public BonusRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
