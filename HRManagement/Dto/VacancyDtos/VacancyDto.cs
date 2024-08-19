using Newtonsoft.Json;

namespace HRManagement.Dto.VacancyDtos
{
    public class VacancyDto
    {
        [JsonProperty("positionId")]
        public int PositionId { get; set; }
        [JsonProperty("departmentId")]
        public int DepartmentId { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
    }
}
