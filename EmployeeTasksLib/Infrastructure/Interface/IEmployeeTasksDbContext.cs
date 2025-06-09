using System.Data.Entity;
using System.Threading.Tasks;
using EmployeeTasksLib.App_Code.DomainModel;
using System;

namespace EmployeeTasksLib.App_Code.Infrastructure.Interface
{
    /// <summary>
    /// Интерфейс для контекста моделей
    /// </summary>
    public interface IEmployeeTasksDbContext : IDisposable
    {
        /// <summary>
        /// Сотрудники
        /// </summary>
        DbSet<Employee> Employee { get; set; }

        /// <summary>
        /// Задачи сотрудников
        /// </summary>
        DbSet<EmployeeTask> EmployeeTask { get; set; }

        /// <summary>
        /// Персоны
        /// </summary>
        DbSet<Person> Person { get; set; }

        /// <summary>
        /// Сохранение изменений в базе данных
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}