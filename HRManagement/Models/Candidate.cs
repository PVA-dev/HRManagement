using HRManagement.Dto.CandidateDtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManagement.Models
{
    public class Candidate : BaseEntity
	{
        public PersonalInfo PersonalInfo { get; set; }
        [ForeignKey("PersonalInfo")]
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
		public string Description { get; set; }

		public void FillFromDto(CandidateDto candidateDto)
		{
            VacancyId = candidateDto.VacancyId;
            ResumeURL = candidateDto.ResumeURL;
            Description = candidateDto.Description;

            if (PersonalInfo == null)
            {
                PersonalInfo = new PersonalInfo();
            }

            PersonalInfo.FirstName = candidateDto.FirstName;
            PersonalInfo.LastName = candidateDto.LastName;
            PersonalInfo.Patronymic = candidateDto.Patronymic;
            PersonalInfo.PassportNumber = candidateDto.PassportNumber;
            PersonalInfo.PassportSeries = candidateDto.PassportSeries;
            PersonalInfo.Phone = candidateDto.Phone;
            PersonalInfo.Email = candidateDto.Email;
            PersonalInfo.Address = candidateDto.Address;
            PersonalInfo.DateOfBirth = candidateDto.DateOfBirth;
        }
    }
}
