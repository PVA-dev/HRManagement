using HRManagement.Data.Interfaces;
using HRManagement.Dto.VacancyDtos;
using HRManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class VacancyController : ControllerBase
	{
		private readonly IVacancyRepository _vacancyRepository;
        private readonly IVacancyStatusRepository _vacancyStatusRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public VacancyController(IVacancyRepository vacancyRepository
                                , IVacancyStatusRepository vacancyStatusRepository
                                , IEmployeeRepository employeeRepository
                                , IPositionRepository positionRepository
                                , IDepartmentRepository departmentRepository)
		{
			_vacancyRepository = vacancyRepository;
            _vacancyStatusRepository = vacancyStatusRepository;
            _employeeRepository = employeeRepository;
            _positionRepository = positionRepository;
            _departmentRepository = departmentRepository;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vacancy = await _vacancyRepository.GetVacanciyFullInfoByIdAsync(id);

            if (vacancy == null)
            {
                return NotFound("Вакансия с таким Id не найдена");
            }

            var vacancyGetDto = new VacancyGetDto();
            vacancyGetDto.FillFromModel(vacancy);

            return Ok(vacancyGetDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VacancyDto vacancyCreateDto)
        {
            var vacancy = new Vacancy();
            vacancy.FillFromDto(vacancyCreateDto);
            var status = await _vacancyStatusRepository.GetByNameAsync("Создана");
            vacancy.StatusId = status.Id;
            vacancy.DateCreated = DateTime.Now;
            vacancy.DateStatusUpdated = DateTime.Now;

            if (await _vacancyRepository.AddAsync(vacancy))
            {
                return Ok("Вакансия успешно создана");
            }
            else
            {
                return StatusCode(500, "Не удалось создать в БД вакансию");
            }
        }

        [HttpPost("SetStatusInProgress/{id}")]
        public async Task<IActionResult> SetStatusInProgress(int id, [FromBody]VacancySetInProgressDto vacInProgressDto)
        {
            var vacancy = await _vacancyRepository.GetByIdAsync(id);
            if (vacancy == null)
            {
                return NotFound("Вакансия с таким Id не найдена");
            }

            if (!await _employeeRepository.HRManagerExistsAsync(vacInProgressDto.HRManagerId))
            {
                return BadRequest("Указан неверный Id hr менеджера");
            }

            var status = await _vacancyStatusRepository.GetByNameAsync("В работе");
            vacancy.StatusId = status.Id;
            vacancy.HRManagerId = vacInProgressDto.HRManagerId;
            vacancy.DateStatusUpdated = DateTime.Now;

            if (await _vacancyRepository.UpdateAsync(vacancy))
            {
                return Ok("Вакансия взята в работу успешно");
            }
            else
            {
                return StatusCode(500, "Не удалось изменить статус вакансии");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VacancyDto vacancyUpdateDto)
        {
            var vacancy = await _vacancyRepository.GetByIdAsync(id);
            if (vacancy == null)
            {
                return NotFound("Вакансия с таким Id не найдена");
            }

            var position = await _positionRepository.GetByIdAsync(vacancyUpdateDto.PositionId);
            if (position == null)
            {
                return BadRequest("Должность с таким Id не найдена");
            }

            var department = await _departmentRepository.GetByIdAsync(vacancyUpdateDto.DepartmentId);
            if (department == null)
            {
                return BadRequest("Отдел с таким Id не найден");
            }

            vacancy.FillFromDto(vacancyUpdateDto);

            if (await _vacancyRepository.UpdateAsync(vacancy))
            {
                return Ok("Данные о вакансии успешно обновлены");
            }
            else
            {
                return StatusCode(500, "Не удалось обновить данные о вакансии");
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var vacancy = await _vacancyRepository.GetByIdAsync(id);

            if (vacancy == null)
            {
                return NotFound("Вакансия с таким Id не найдена");
            }

            if (await _vacancyRepository.DeleteAsync(vacancy))
            {
                return Ok("Вакансия успешно удалена");
            }
            else
            {
                return StatusCode(500, "Не удалось удалить вакансию");
            }
        }
    }
}
