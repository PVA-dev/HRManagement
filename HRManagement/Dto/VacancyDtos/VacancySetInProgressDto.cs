using Newtonsoft.Json;

namespace HRManagement.Dto.VacancyDtos
{
    public class VacancySetInProgressDto
    {
        [JsonProperty("hrManagerId")]
        public int HRManagerId { get; set; }    
    }
}
