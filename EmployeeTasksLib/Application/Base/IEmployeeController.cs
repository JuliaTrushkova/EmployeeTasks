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
        Task<LoginOutputDto> CheckLoginAsync(string email, string password, CancellationToken cancellationToken);

        Task CreateEmployeeAsync(EmployeeCreateDto employeeDto, CancellationToken cancellationToken);
    }
}