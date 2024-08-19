using HRManagement.Data.Interfaces;
using HRManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Data.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
	{
		public EmployeeRepository(AppDbContext dbContext) : base(dbContext)
		{
		}

		public async Task<Employee?> GetByPassportInfoAsync(string passportSeries, string passportNumber)
		{
			return await _dbContext.Employees.Include(pi => pi.PersonalInfo)
								.FirstOrDefaultAsync(x => x.PersonalInfo.PassportNumber == passportNumber 
														&& x.PersonalInfo.PassportSeries == passportSeries);
		}

		public async Task<Employee?> GetIncludePersonalInfoByIdAsync(int id)
		{
			return await _dbContext.Employees.Include(pi => pi.PersonalInfo).FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<Employee>> GetAllEmployeesIncludePersonalInfoAsync()
		{
			return await _dbContext.Employees.Include(pi => pi.PersonalInfo).ToListAsync();
		}

		public async Task<bool> DeleteWithPersonalInfoAsync(Employee employee)
		{
			using (var transaction = await _dbContext.Database.BeginTransactionAsync())
			{
				try
				{
                    _dbContext.Employees.Remove(employee);
                    //await SaveAsync();

                    var personalInfo = await _dbContext.PersonalInfos.FirstOrDefaultAsync(x => x.Id == employee.PersonalInfoId);
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

        public async Task<bool> HRManagerExistsAsync(int id)
        {
            return await _dbContext.Employees
									.Include(x => x.Position)
									.AnyAsync(x => x.Id == id && x.Position.Name == "HR менеджер");
        }
    }
}
