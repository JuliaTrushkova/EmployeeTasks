using EmployeeTasksLib.App_Code.Application.Base;
using EmployeeTasksLib.App_Code.DomainModel;
using EmployeeTasksLib.App_Code.Infrastructure.Interface;
using EmployeeTasksLib.App_Code.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeTasksLib.App_Code.Application
{
    /// <summary>
    /// Хелпер для работы с задачами сотрудников
    /// </summary>
    public class EmployeeTaskController : IEmployeeTaskController
    {
        private readonly IEmployeeTasksDbContext _dbContext;
        public EmployeeTaskController(IEmployeeTasksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Получение списка задач сотрудников из базы данных
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<EmployeeTaskListDto> GetEmployeeTasksListAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            List<EmployeeTask> employeeTasks = await _dbContext.EmployeeTask.Include(et => et.Employee).ToListAsync();
            return MappingHelper.MapEmployeeTaskListFromDomToDto(employeeTasks);
        }

        /// <summary>
        /// Создание задачи сотрудника в базе данных
        /// </summary>
        /// <param name="employeeTaskDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task CreateEmployeeTaskAsync(EmployeeTaskCreateDto employeeTaskDTO, CancellationToken cancellationToken)
        {
            try
            {
                var employeeTask = MappingHelper.MapEmployeeTaskCreateFromDtoToDom(employeeTaskDTO);
                cancellationToken.ThrowIfCancellationRequested();

                _dbContext.EmployeeTask.Add(employeeTask);
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