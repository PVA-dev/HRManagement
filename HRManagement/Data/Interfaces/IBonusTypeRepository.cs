using HRManagement.Models;

namespace HRManagement.Data.Interfaces
{
    public interface IBonusTypeRepository : IRepository<BonusType>
    {
        Task<BonusType?> GetByNameAsync(string name);
    }
}
