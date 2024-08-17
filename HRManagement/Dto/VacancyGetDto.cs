using HRManagement.Models;
using Newtonsoft.Json;

namespace HRManagement.Dto
{
	public class VacancyGetDto
	{
		[JsonProperty("position")]
		public string PositionName { get; set; }

		[JsonProperty("dateCreated")]
		public DateTime DateCreated { get; set; }
		[JsonProperty("dateClosed")]
		public DateTime? DateClosed { get; set; }
		[JsonProperty("hrManager")]
		public string HRManagerName { get; set; }
		[JsonProperty("department")]
		public string DepartmentName { get; set; }
		[JsonProperty("status")]
		public string StatusName { get; set; }

		public void FillFromModel(Vacancy vacancy)
		{
			PositionName = vacancy.Position.Name;
			DateCreated = vacancy.DateCreated;
			DateClosed = vacancy.DateClosed;
			HRManagerName = $"{vacancy.HRManager?.PersonalInfo.LastName} {vacancy.HRManager?.PersonalInfo.FirstName}";
			DepartmentName = vacancy.Department.Name;
			StatusName = vacancy.Status.Name;
		}
	}
}
