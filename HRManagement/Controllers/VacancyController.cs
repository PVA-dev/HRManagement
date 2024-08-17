using HRManagement.Data.Interfaces;
using HRManagement.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VacancyController : ControllerBase
	{
		private readonly IVacancyRepository _vacancyRepository;

		public VacancyController(IVacancyRepository vacancyRepository)
		{
			_vacancyRepository = vacancyRepository;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var vacancies = await _vacancyRepository.GetAllVacanciesFullInfoAsync();
			var vacanciesDto = new List<VacancyGetDto>();

			vacanciesDto.AddRange(vacancies.Select(x =>
			{
				var vacancyGetDto = new VacancyGetDto();
				vacancyGetDto.FillFromModel(x);
				return vacancyGetDto;
			}));

			return Ok(vacanciesDto);
		}
	}
}
