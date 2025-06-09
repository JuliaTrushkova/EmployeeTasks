using EmployeeTasksLib.App_Code.Presentation.Models;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeTasksLib.App_Code.Application.Base
{
    /// <summary>
    /// Интерфейс для работы с задачами сотрудников
    /// </summary>
    public interface IEmployeeTaskController
    {
        /// <summary>
        /// Получение списка задач сотрудников из базы данных
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<EmployeeTaskListDto> GetEmployeeTasksListAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Создание задачи сотрудника в базе данных
        /// </summary>
        /// <param name="employeeTaskDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CreateEmployeeTaskAsync(EmployeeTaskCreateDto employeeTaskDTO, CancellationToken cancellationToken);
    }
}