using HRManagement.Dto.VacancyDtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRManagement.Models
{
	public class Vacancy : BaseEntity
	{
		public Position Position { get; set; }
		[ForeignKey("Position")]
		public int PositionId { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateStatusUpdated { get; set; }
		public DateTime? DateClosed { get; set; }
		public Employee? HRManager { get; set; }
		[ForeignKey("Employee")]
		public int? HRManagerId { get; set; }
		public Department Department { get; set; }
		[ForeignKey("Department")]
		public int DepartmentId { get; set; }
		public VacancyStatus Status { get; set; }
		[ForeignKey("VacancyStatus")]
		public int StatusId { get; set; }
		public string Description { get; set; }

        public void FillFromDto(VacancyDto vacancyCreateDto)
        {
            PositionId = vacancyCreateDto.PositionId;
			DepartmentId = vacancyCreateDto.DepartmentId;
			Description = vacancyCreateDto.Description;
        }
    }
}