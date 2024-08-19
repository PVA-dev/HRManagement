using HRManagement.Dto.PersonalInfoDtos;
using Newtonsoft.Json;

namespace HRManagement.Dto.CandidateDtos
{
    public class CandidateDto : PersonalInfoDto
    {
        [JsonProperty("vacancyId")]
        public int VacancyId { get; set; }
        [JsonProperty("resumeURL")]
        public string? ResumeURL { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
        [JsonProperty("statusId")]
        public int StatusId { get; set; }
    }
}
