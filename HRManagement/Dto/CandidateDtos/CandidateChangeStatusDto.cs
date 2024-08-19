using Newtonsoft.Json;

namespace HRManagement.Dto.CandidateDtos
{
    public class CandidateChangeStatusDto
    {
        [JsonProperty("statusId")]
        public int StatusId { get; set; }
        [JsonProperty("probationChiefId")]
        public int? ProbationChiefId { get; set; }
        [JsonProperty("probationSalary")]
        public int? ProbationSalary { get; set; }
        [JsonProperty("salary")]
        public int? Salary { get; set; }
    }
}
