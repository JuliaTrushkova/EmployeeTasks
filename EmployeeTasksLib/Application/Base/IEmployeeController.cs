using EmployeeTasksLib.App_Code.Presentation.Models;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeTasksLib.App_Code.Application.Base
{
    /// <summary>
    /// Интерфейс для работы с сотрудниками
    /// </summary>
    public interface IEmployeeController
    {
        /// <summary>
        /// Проверка логина и пароля сотрудника в базе данных
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<LoginOutputDto> CheckLoginAsync(string email, string password, CancellationToken cancellationToken);

        /// <summary>
        /// Создание нового сотрудника в базе данных
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CreateEmployeeAsync(EmployeeCreateDto employeeDto, CancellationToken cancellationToken);
    }
}