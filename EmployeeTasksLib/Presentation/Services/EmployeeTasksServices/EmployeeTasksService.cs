using EmployeeTasksLib.App_Code.Application.Base;
using EmployeeTasksLib.App_Code.Presentation.Models;
using EmployeeTasksLib.Presentation.Exceptions;
using EmployeeTasksLib.Presentation.Models;
using System;
using System.Globalization;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeTasksLib.App_Code.Presentation.Services.EmployeeTasksServices
{
    /// <summary>
    /// Сервис по работе с задачами сотрудников
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class EmployeeTasksService : IEmployeeTasksService
    {
        private IEmployeeTaskController _taskController;

        public EmployeeTasksService(IEmployeeTaskController taskController)
        {
            _taskController = taskController;
        }

        /// <summary>
        /// Метод создания новой задачи сотрудника
        /// </summary>
        /// <param name="employeeTask"></param>
        /// <returns></returns>
        public async Task<WcfResult<string>> CreateNewTaskAsync(EmployeeTaskCreateDto employeeTask)
        {
            Console.WriteLine($"Начало CreateNewTaskAsync");

            try
            {
                CheckTaskDto(employeeTask);

                using (var cts = new CancellationTokenSource(TimeSpan.FromMinutes(1))) // timeout 1 минута
                {
                    await _taskController.CreateEmployeeTaskAsync(employeeTask, cts.Token);
                    return new WcfResult<string>
                    {
                        Success = true,
                        Data = "Задача успешно создана"
                    };
                }
            }
            catch (Exception ex)
            {
                return ExceptionHelper.CreateComplexErrorResult<string>
                    (ex, "Ошибка при создании задачи");
            }
        }

        /// <summary>
        /// Метод получения списка задач сотрудников
        /// </summary>
        /// <returns></returns>
        public async Task<WcfResult<EmployeeTaskListDto>> GetListAsync()
        {
            Console.WriteLine($"Начало GetListAsync");

            try
            {
                using (var cts = new CancellationTokenSource(TimeSpan.FromMinutes(1))) // timeout 1 минута
                {
                    var result = await _taskController.GetEmployeeTasksListAsync(cts.Token);
                    return new WcfResult<EmployeeTaskListDto>
                    {
                        Success = true,
                        Data = result
                    };
                }
            }
            catch (Exception ex)
            {
                return ExceptionHelper.CreateComplexErrorResult<EmployeeTaskListDto>
                   (ex, "Ошибка при получении списка задач");
            }
        }

        private static void CheckTaskDto(EmployeeTaskCreateDto employeeTask)
        {
            if (DateTime.TryParseExact(employeeTask.DateCompleted, "dd.MM.yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateCompleted))
            {
                if (dateCompleted.Date != DateTime.Now.Date)
                {
                    throw new WebFaultException<string>("Можно создавать задачи только на текущую дату", HttpStatusCode.BadRequest);
                }
            }
            else
            {

            }

            if (TimeSpan.TryParse(employeeTask.TimeSpent, out TimeSpan timeSpent))
            {
                if (timeSpent.TotalMinutes <= 0)
                {
                    throw new WebFaultException<string>("Время выполнения задачи должно быть больше нуля", HttpStatusCode.BadRequest);
                }
            }
            else
            {
                throw new WebFaultException<string>("Некорректный формат времени выполнения задачи", HttpStatusCode.BadRequest);
            }
        }
    }
}
