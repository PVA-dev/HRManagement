using HRManagement.Data.Interfaces;
using HRManagement.Models;

namespace HRManagement.Data.Repositories
{
    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        public PositionRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
