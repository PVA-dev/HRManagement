using HRManagement.Models;

namespace HRManagement.Data
{
	public class Seed
	{
		public static void SeedData(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

				context.Database.EnsureCreated();

				if (!context.BonusTypes.Any())
				{
					context.BonusTypes.AddRange(new List<BonusType>()
					{
						new BonusType()
						{
							Name = "Премия за закрытие вакансии",
							Sum = 5000
						},
						new BonusType()
						{
							Name = "Премия за перевыполнение плана",
							Sum = 4000
						}
					});
				}

				if (!context.CandidateStatuses.Any())
				{
					context.CandidateStatuses.AddRange(new List<CandidateStatus>()
					{
						new CandidateStatus()
						{
							Name = "Откликнулся на вакансию",
						},
						new CandidateStatus()
						{
							Name = "Найден на портале",
						},
						new CandidateStatus()
						{
							Name = "Прошел телефонное интервью",
						},
						new CandidateStatus()
						{
							Name = "Прошел техническое собеседование",
						},
						new CandidateStatus()
						{
							Name = "Прошел собеседование с директором",
						},
						new CandidateStatus()
						{
							Name = "Принят на испытательный срок",
						},
						new CandidateStatus()
						{
							Name = "Принят в компанию",
						},
						new CandidateStatus()
						{
							Name = "Не подходит",
						}
					});
				}

				if (!context.VacancyStatuses.Any())
				{
					context.VacancyStatuses.AddRange(new List<VacancyStatus>()
					{
						new VacancyStatus()
						{
							Name = "Создана"
						},
						new VacancyStatus()
						{
							Name = "В работе"
						},
						new VacancyStatus()
						{
							Name = "Закрыта"
						}
					});
				}

				if (!context.Positions.Any())
				{
					context.Positions.AddRange(new List<Position>()
					{
						new Position()
						{
							Name = "Директор"
						},
						new Position()
						{
							Name = "Начальник отдела продаж"
						},
						new Position()
						{
							Name = "Начальник отдела разработки"
						},
						new Position()
						{
							Name = "Начальник отдела HR"
						},
						new Position()
						{
							Name = "Начальник аналитического отдела"
						},
						new Position()
						{
							Name = "Программист"
						},
						new Position()
						{
							Name = "HR менеджер"
						},
						new Position()
						{
							Name = "Аналитик"
						},
						new Position()
						{
							Name = "Менеджер по продажам"
						},
						new Position()
						{
							Name = "Юрист"
						}
					});
				}

				if (!context.Departments.Any())
				{
					context.Departments.AddRange(new List<Department>()
					{
						new Department()
						{
							Name = "Директорат"
						},
						new Department()
						{
							Name = "Отдел автоматизации ЭДО"
						},
						new Department()
						{
							Name = "Отдел HR"
						},
						new Department()
						{
							Name = "Отдел продаж Nestle"
						},
						new Department()
						{
							Name = "Отдел продаж Purina"
						},
						new Department()
						{
							Name = "Отдел продаж Horeca"
						},
						new Department()
						{
							Name = "Отдел продаж P&G"
						},
						new Department()
						{
							Name = "Отдел аналитики"
						},
						new Department()
						{
							Name = "Отдел развития ИС"
						},
						new Department()
						{
							Name = "Юридический отдел"
						},
					});
				}

				context.SaveChanges();
			}
		}
	}
}
