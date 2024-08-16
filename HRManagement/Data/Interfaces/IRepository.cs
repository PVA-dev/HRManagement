using HRManagement.Models;

namespace HRManagement.Data.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<bool> SaveAsync();
        Task<bool> UpdateAsync(T entity);
    }
}
