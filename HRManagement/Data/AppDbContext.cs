using HRManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Bonus> Bonuses { get; set; }
        public DbSet<BonusType> BonusTypes { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateStatus> CandidateStatuses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PersonalInfo> PersonalInfos { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<VacancyStatus> VacancyStatuses { get; set; }   
    }
}
