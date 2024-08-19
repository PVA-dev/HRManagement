using HRManagement.Dto.PersonalInfoDtos;
using HRManagement.Models;
using Newtonsoft.Json;

namespace HRManagement.Dto.CandidateDtos
{
    public class CandidateGetDto : PersonalInfoDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("vacancyDescription")]
        public string VacancyDescription { get; set; }
        [JsonProperty("resumeURL")]
        public string? ResumeURL { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        public void FillFromModel(Candidate candidate)
        {
            Id = candidate.Id;
            Description = candidate.Description;
            VacancyDescription = candidate.Vacancy.Description;
            ResumeURL = candidate.ResumeURL;
            Status = candidate.Status.Name;
            Address = candidate.PersonalInfo.Address;
            DateOfBirth = candidate.PersonalInfo.DateOfBirth;
            Email = candidate.PersonalInfo.Email;
            FirstName = candidate.PersonalInfo.FirstName;
            LastName = candidate.PersonalInfo.LastName;
            PassportNumber = candidate.PersonalInfo.PassportNumber;
            PassportSeries = candidate.PersonalInfo.PassportSeries;
            Patronymic = candidate.PersonalInfo.Patronymic;
            Phone = candidate.PersonalInfo.Phone;
        }
    }
}
