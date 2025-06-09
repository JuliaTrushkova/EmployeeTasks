using EmployeeTasksLib.App_Code.DomainModel;
using EmployeeTasksLib.App_Code.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EmployeeTasksLib.App_Code.Application
{
	/// <summary>
	/// Маппинг моделей Dto в модели домена и обратно
	/// </summary>
	public static class MappingHelper
	{
		/// <summary>
		/// Маппинг Dto модели сотрудника в модель домена
		/// </summary>
		/// <param name="employeeDto"></param>
		/// <returns></returns>
		public static Employee MapEmployeeFromDtoToDom(EmployeeCreateDto employeeDto)
		{
			//Person person = new Person(employeeDto.FirstName, employeeDto.LastName);
			Employee employee = new Employee()
			{
				Email = employeeDto.Email,
				Password = employeeDto.Password,
				Person = new Person()
                {
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName,
                }
            };
			return employee;
		}

        /// <summary>
        /// Маппинг Dto модели задачи сотрудника в модель домена
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        public static EmployeeTask MapEmployeeTaskCreateFromDtoToDom(EmployeeTaskCreateDto employeeDto)
        {
            DateTime dateCompleted = DateTime.ParseExact(employeeDto.DateCompleted, "dd.MM.yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None);

            return new EmployeeTask()
			{
				id_Employee = employeeDto.id_Employee,
				Description = employeeDto.Description,
				TimeSpent = employeeDto.TimeSpent,
                DateCompleted = dateCompleted
            };
        }

        /// <summary>
        /// Маппинг доменной модели сотрудника в модель Dto логина
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public static LoginOutputDto MapEmployeeLoginFromDomToDto(Employee employee)
        {
            return new LoginOutputDto()
            {
                id_Employee = employee.id_Employee,
                FIO_Employee = employee.Person.ToString()
            };
        }

        /// <summary>
        /// Маппинг доменной модели задачи сотрудника в модель Dto
        /// </summary>
        /// <param name="employeeTask"></param>
        /// <returns></returns>
        public static EmployeeTaskListItemDto MapEmployeeTaskFromDomToDto(EmployeeTask employeeTask)
        {
			return new EmployeeTaskListItemDto()
			{
				id_EmployeeTask = employeeTask.id_EmployeeTask,
				FIO_Employee = employeeTask.Employee.Person.ToString(),
				Description = employeeTask.Description,
				TimeSpent = employeeTask.TimeSpent,
				DateCompleted = employeeTask.DateCompleted.ToString("dd.MM.yyyy")
            };
        }

        /// <summary>
        /// Маппинг доменной модели списка задач сотрудника в модель Dto
        /// </summary>
        /// <param name="employeeTasks"></param>
        /// <returns></returns>
        public static EmployeeTaskListDto MapEmployeeTaskListFromDomToDto(List<EmployeeTask> employeeTasks)
        {
            return new EmployeeTaskListDto()
			{
				EmployeeTasks = employeeTasks.Select(task => MapEmployeeTaskFromDomToDto(task)).ToList(),
                TotalTimeSpent = GetTotalTimeSpent(employeeTasks)
            };
        }

        /// <summary>
        /// Получение общего времени выполнения всех задач
        /// </summary>
        /// <param name="employeeTasks"></param>
        /// <returns></returns>
        private static string GetTotalTimeSpent(List<EmployeeTask> employeeTasks)
        {
            if (employeeTasks == null || employeeTasks.Count < 1)
            {
                return "0 ч 0 мин";
            }

            TimeSpan totalTime = new TimeSpan();

            foreach (var employeeTask in employeeTasks)
            {
                TimeSpan timeSpent = TimeSpan.Parse(employeeTask.TimeSpent);
                totalTime = totalTime.Add(timeSpent);
            }

            string result = totalTime.Days > 0 ? totalTime.Days + " дн. " : "";
            result += totalTime.Hours > 0 ? totalTime.Hours + " ч " : "";
            result += totalTime.Minutes > 0 ? totalTime.Minutes + " мин " : "";
            return result;
        }
    }
}