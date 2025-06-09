using EmployeeTasksLib.App_Code.Application.Base;
using EmployeeTasksLib.App_Code.Infrastructure.Interface;
using EmployeeTasksLib.App_Code.Presentation.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeTasksLib.App_Code.Application
{
    /// <summary>
    /// Хелпер для работы с сотрудниками
    /// </summary>
    public class EmployeeController : IEmployeeController
    {
        private readonly IEmployeeTasksDbContext _dbContext;
        public EmployeeController(IEmployeeTasksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Проверка логина и пароля сотрудника в базе данных
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<LoginOutputDto> CheckLoginAsync(string email, string password, CancellationToken cancellationToken)
        {            
            cancellationToken.ThrowIfCancellationRequested();
            var employee = await _dbContext.Employee.FirstOrDefaultAsync(x =>
                                                x.Password.ToLower() == password.ToLower()
                                                && x.Email.ToLower() == email.ToLower()).ConfigureAwait(false);
            if (employee == null)
            {
                throw new Exception("Неверная пара логин/пароль");
            }

            return MappingHelper.MapEmployeeLoginFromDomToDto(employee);
        }

        /// <summary>
        /// Создание нового сотрудника в базе данных
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task CreateEmployeeAsync(EmployeeCreateDto employeeDto, CancellationToken cancellationToken)
        {
            try
            {
                var employee = MappingHelper.MapEmployeeFromDtoToDom(employeeDto);
                cancellationToken.ThrowIfCancellationRequested();

                _dbContext.Employee.Add(employee);
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateException ex)
            {
                // Детальная диагностика
                Console.WriteLine($"DbUpdateException: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner: {ex.InnerException.Message}");
                    if (ex.InnerException.InnerException != null)
                    {
                        Console.WriteLine($"SQL: {ex.InnerException.InnerException.Message}");
                    }
                }
                throw;
            }
        }
    }
}