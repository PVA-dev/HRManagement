using Newtonsoft.Json;

namespace HRManagement.Dto.PersonalInfoDtos
{
    public class PersonalInfoDto
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("patronymic")]
        public string? Patronymic { get; set; }
        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("passportSeries")]
        public string PassportSeries { get; set; }
        [JsonProperty("passportNumber")]
        public string PassportNumber { get; set; }
    }
}
