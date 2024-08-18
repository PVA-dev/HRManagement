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

				if (!context.Employees.Any())
				{
                    var mainChief = new Employee()
                    {
                        DateStartWork = new DateTime(2020, 2, 2),
                        DepartmentId = context.Departments.First(x => x.Name == "Директорат").Id,
                        PositionId = context.Positions.First(x => x.Name == "Директор").Id,
                        Salary = 500000,
                        PersonalInfo = new PersonalInfo()
                        {
                            DateOfBirth = new DateTime(1986, 12, 12),
                            Address = "ул. Пушкина д. 1",
                            Email = "myemail1@mail.ru",
                            FirstName = "Иван",
                            LastName = "Иванов",
                            Patronymic = "Иванович",
                            PassportNumber = "11111111111",
                            PassportSeries = "7505",
                            Phone = "89213333311"
                        }
                    };

                    context.Employees.Add(mainChief);
                    context.SaveChanges();

                    var hrChief = new Employee()
					{
						ChiefId = mainChief.Id,
						DateStartWork = new DateTime(2020, 1, 1),
						DepartmentId = context.Departments.First(x => x.Name == "Отдел HR").Id,
						PositionId = context.Positions.First(x => x.Name == "Начальник отдела HR").Id,
						Salary = 120000,
						PersonalInfo = new PersonalInfo()
						{
							DateOfBirth = new DateTime(1986, 1, 1),
							Address = "ул. Пушкина д. 2",
							Email = "myemail2@mail.ru",
							FirstName = "Семен",
							LastName = "Семенов",
							Patronymic = "Семенович",
							PassportNumber = "11111111112",
							PassportSeries = "7515",
							Phone = "89213333312"
						}
					};

                    context.Employees.Add(hrChief);
                    context.SaveChanges();

                    var hrManager = new Employee()
                    {
                        ChiefId = hrChief.Id,
                        DateStartWork = new DateTime(2020, 1, 1),
                        DepartmentId = context.Departments.First(x => x.Name == "Отдел HR").Id,
                        PositionId = context.Positions.First(x => x.Name == "HR менеджер").Id,
                        Salary = 80000,
                        PersonalInfo = new PersonalInfo()
                        {
                            DateOfBirth = new DateTime(1986, 3, 3),
                            Address = "ул. Пушкина д. 3",
                            Email = "myemail3@mail.ru",
                            FirstName = "Мария",
                            LastName = "Семенова",
                            Patronymic = "Семеновна",
                            PassportNumber = "11111111113",
                            PassportSeries = "7535",
                            Phone = "89213333313"
                        }
                    };

                    context.Employees.Add(hrManager);

                    var hrManager1 = new Employee()
                    {
                        ChiefId = hrChief.Id,
                        DateStartWork = new DateTime(2020, 5, 5),
                        DepartmentId = context.Departments.First(x => x.Name == "Отдел HR").Id,
                        PositionId = context.Positions.First(x => x.Name == "HR менеджер").Id,
                        Salary = 80000,
                        PersonalInfo = new PersonalInfo()
                        {
                            DateOfBirth = new DateTime(1986, 5, 5),
                            Address = "ул. Пушкина д. 4",
                            Email = "myemail4@mail.ru",
                            FirstName = "Елена",
                            LastName = "Петрова",
                            Patronymic = "Семеновна",
                            PassportNumber = "11111111113",
                            PassportSeries = "7555",
                            Phone = "89213333314"
                        }
                    };

					context.Employees.Add(hrManager1);
					context.SaveChanges();
                }
			}
		}
	}
}
