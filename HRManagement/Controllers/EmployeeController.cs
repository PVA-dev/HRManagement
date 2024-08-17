using HRManagement.Data.Interfaces;
using HRManagement.Dto;
using HRManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeeRepository _employeeRepository;

		public EmployeeController(IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] EmployeeDto employeeDto)
		{
			var employee = await _employeeRepository.GetByPassportInfoAsync(employeeDto.PassportSeries, employeeDto.PassportNumber);

			if (employee != null)
			{
				return BadRequest("Сотрудник с такими паспортными данными уже существует");
			}

			employee = new Employee();
			employee.FillFromDto(employeeDto);

			if (await _employeeRepository.AddAsync(employee))
			{
				return Ok("Сотрудник успешно создан");
			}
			else
			{
				return StatusCode(500, "Не удалось создать в БД сотрудника");
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var employee = await _employeeRepository.GetIncludePersonalInfoById(id);

			if (employee == null)
			{
				return NotFound("Сотрудника с таким id не существует");
			}

			var employeeDto = new EmployeeDto();
			employeeDto.FillFromModel(employee);
			return	Ok(employeeDto);
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var employees = await _employeeRepository.GetAllEmployeesIncludePersonalInfo();
			var employeesDto = new List<EmployeeDto>();

			employeesDto.AddRange(employees.Select(x =>
			{
				var employeeDto = new EmployeeDto();
				employeeDto.FillFromModel(x);
				return employeeDto;
			}));

			return Ok(employeesDto);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody]EmployeeDto employeeDto)
		{
			var employee = await _employeeRepository.GetIncludePersonalInfoById(id);

			if (employee == null)
			{
				return BadRequest("Сотрудник с таким Id не найден");
			}

			employee.FillFromDto(employeeDto);

			if (await _employeeRepository.UpdateAsync(employee))
			{
				return Ok("Данные о сотруднике успешно обновлены");
			}
			else
			{
				return StatusCode(500, "Не удалось обновить данные о сотруднике");
			}
		}

		[HttpDelete("id")]
		public async Task<IActionResult> Delete(int id)
		{
			var employee = await _employeeRepository.GetByIdAsync(id);

			if (employee == null)
			{
				return NotFound("Сотрудника с таким id не существует");
			}

			if (await _employeeRepository.DeleteWithPersonalInfoAsync(employee))
			{
				return Ok("Сотрудник успешно удален");
			}
			else
			{
				return StatusCode(500, "Не удалось удалить сотрудника");
			}
		}
	}
}
