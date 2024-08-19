using HRManagement.Data.Interfaces;
using HRManagement.Models;

namespace HRManagement.Data.Repositories
{
	public class PersonalInfoRepository : Repository<PersonalInfo>, IPersonalInfoRepository
	{
		public PersonalInfoRepository(AppDbContext dbContext) : base(dbContext)
		{
		}
	}
}
