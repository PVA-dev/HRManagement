using HRManagement.Data.Interfaces;
using HRManagement.Models;

namespace HRManagement.Data.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext dbContext):base(dbContext) { }
    }
}
