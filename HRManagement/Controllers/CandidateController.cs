using HRManagement.Data.Interfaces;
using HRManagement.Dto.CandidateDtos;
using HRManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICandidateStatusRepository _candidateStatusRepository;
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBonusRepository _bonusRepository;
        private readonly IBonusTypeRepository _bonusTypeRepository;
        private readonly IVacancyStatusRepository _vacancyStatusRepository;

        public CandidateController(ICandidateRepository candidateRepository
                                    , ICandidateStatusRepository candidateStatusRepository
                                    , IVacancyRepository vacancyRepository
                                    , IEmployeeRepository employeeRepository
                                    , IBonusRepository bonusRepository
                                    , IBonusTypeRepository bonusTypeRepository
                                    , IVacancyStatusRepository vacancyStatusRepository)
        {
            _candidateRepository = candidateRepository;
            _candidateStatusRepository = candidateStatusRepository;
            _vacancyRepository = vacancyRepository;
            _employeeRepository = employeeRepository;
            _bonusRepository = bonusRepository;
            _bonusTypeRepository = bonusTypeRepository;
            _vacancyStatusRepository = vacancyStatusRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CandidateDto candidateDto)
        {
            var candidate = await _candidateRepository.GetByPassportInfoAsync(candidateDto.PassportSeries, candidateDto.PassportNumber);

            if (candidate != null)
            {
                return BadRequest("Кандидат с такими паспортными данными уже существует");
            }

            var fromVacancyStatus = await _candidateStatusRepository.GetByNameAsync("Откликнулся на вакансию");
            var foundInServiceStatus = await _candidateStatusRepository.GetByNameAsync("Найден на портале");

            if (candidateDto.StatusId != fromVacancyStatus?.Id && candidateDto.StatusId != foundInServiceStatus?.Id)
            {
                return BadRequest("Начальный статус кандидата может быть только \"Откликнулся на вакансию\" или \"Найден на портале\"");
            }

            var vacancy = await _vacancyRepository.GetFullInfoByIdAsync(candidateDto.VacancyId);
            if (vacancy == null)
            {
                return BadRequest("Вакансия с таким Id не найдена");
            }

            if (vacancy.Status.Name != "В работе")
            {
                return BadRequest("Вакансия должна быть в статусе \"В работе\"");
            }

            candidate = new Candidate();
            candidate.FillFromDto(candidateDto);
            candidate.StatusId = candidateDto.StatusId;
            candidate.DateStatusChanged = DateTime.Now;

            if (await _candidateRepository.AddAsync(candidate))
            {
                return Ok("Кандидат успешно создан");
            }
            else
            {
                return StatusCode(500, "Не удалось создать в БД кандидата");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var candidates = await _candidateRepository.GetAllIncludeFullInfoAsync();
            var candidatesDto = new List<CandidateGetDto>();

            candidatesDto.AddRange(candidates.Select(x =>
            {
                var candidateGetDto = new CandidateGetDto();
                candidateGetDto.FillFromModel(x);
                return candidateGetDto;
            }));

            return Ok(candidatesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var candidate = await _candidateRepository.GetIncludeFullInfoByIdAsync(id);

            if (candidate == null)
            {
                return NotFound("Кандидат с таким Id не найден");
            }

            var candidateGetDto = new CandidateGetDto();
            candidateGetDto.FillFromModel(candidate);

            return Ok(candidateGetDto);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);
            if (candidate == null)
            {
                return NotFound("Кандидата с таким id не существует");
            }

            if (await _candidateRepository.DeleteWithPersonalInfoAsync(candidate))
            {
                return Ok("Кандидат успешно удален");
            }
            else
            {
                return StatusCode(500, "Не удалось удалить кандидата");
            }
        }

        [HttpPost("ChangeStatus/{id}")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] CandidateChangeStatusDto candidateChangeStatusDto)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);
            if (candidate == null) 
            {
                return NotFound("Кандидата с таким id не существует");
            }

            var status = await _candidateStatusRepository.GetByIdAsync(candidateChangeStatusDto.StatusId);
            if (status == null)
            {
                return BadRequest("Статус с таким Id не найден");
            }

            if (status.Name == "Принят на испытательный срок" )
            {
                if (!(candidateChangeStatusDto.ProbationSalary.HasValue && candidateChangeStatusDto.ProbationSalary.Value > 0 && candidateChangeStatusDto.ProbationChiefId.HasValue))
                {
                    return BadRequest("Необходимо указать наставника и зарплату на испытательный срок");
                }

                var probationChief = await _employeeRepository.GetByIdAsync(candidateChangeStatusDto.ProbationChiefId.Value);
                if (probationChief == null)
                {
                    return BadRequest("Указан неверный Id наставника");
                }

                candidate.ProbationChiefId = probationChief.Id;
                candidate.ProbationSalary = candidateChangeStatusDto.ProbationSalary;
            }

            // Если кандидат принят в компанию, создаем сотрудника с его персональными данными и начисляем бонус HRМенеджеру
            if (status.Name == "Принят в компанию")
            {
                //Добавляем нового сотрудника
                var vacancy = await _vacancyRepository.GetByIdAsync(candidate.VacancyId);
                if (vacancy == null)
                {
                    return BadRequest("Вакансия удалена");
                }

                var employee = new Employee()
                {
                    DateStartWork = DateTime.Now,
                    PersonalInfoId = candidate.PersonalInfoId,
                    DepartmentId = vacancy.DepartmentId,
                    PositionId = vacancy.PositionId,
                    Salary = candidateChangeStatusDto.Salary.HasValue && candidateChangeStatusDto.Salary > 0 
                                                    ? candidateChangeStatusDto.Salary.Value 
                                                    : candidate.ProbationSalary.Value
                };

                await _employeeRepository.AddAsync(employee);

                //Закрываем вакансию
                var vacancyStatus = await _vacancyStatusRepository.GetByNameAsync("Закрыта");
                vacancy.StatusId = vacancyStatus.Id;
                await _vacancyRepository.UpdateAsync(vacancy);

                //Создаем бонус HRМенеджеру
                var bonusType = await _bonusTypeRepository.GetByNameAsync("Премия за закрытие вакансии");
                var bonus = new Bonus()
                {
                    BonusTypeId = bonusType.Id,
                    DateCreated = DateTime.Now,
                    EmployeeId = vacancy.HRManagerId.Value
                };
                
                await _bonusRepository.AddAsync(bonus);
            }

            candidate.StatusId = status.Id;
            candidate.DateStatusChanged = DateTime.Now;
            await _candidateRepository.UpdateAsync(candidate);
            return Ok("Статус кандидата успешно обновлен");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CandidateDto candidateDto)
        {
            var candidate = await _candidateRepository.GetIncludeFullInfoByIdAsync(id);
            if (candidate == null)
            {
                return NotFound("Кандидат с таким Id не найден");
            }

            candidate.FillFromDto(candidateDto);

            if (await _candidateRepository.UpdateAsync(candidate))
            {
                return Ok("Данные о кандидате успешно обновлены");
            }
            else
            {
                return StatusCode(500, "Не удалось обновить данные о кандидате");
            }
        }
    }
}
