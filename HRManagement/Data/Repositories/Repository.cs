using HRManagement.Data.Interfaces;
using HRManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddAsync(T entity)
        {
            _dbContext.Add(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);
            return await SaveAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _dbContext.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbContext.Update(entity);
            return await SaveAsync();
        }
    }
}
