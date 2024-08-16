using System.ComponentModel.DataAnnotations.Schema;

namespace HRManagement.Models
{
	public class Candidate : BaseEntity
	{
        public PersonalInfo PersonalInfo { get; set; }
        public int PersonalInfoId { get; set; }
        public Vacancy Vacancy { get; set; }
		[ForeignKey("Vacancy")]
		public int VacancyId { get; set; }
		public CandidateStatus Status { get; set; }
		[ForeignKey("CandidateStatus")]
		public int StatusId { get; set; }
		public DateTime DateStatusChanged { get; set; }
		public Employee? ProbationChief { get; set; }
		[ForeignKey("Employee")]
		public int? ProbationChiefId { get; set; }
		public int? ProbationSalary { get; set; }
		public string ResumeURL { get; set; }
	}
}
