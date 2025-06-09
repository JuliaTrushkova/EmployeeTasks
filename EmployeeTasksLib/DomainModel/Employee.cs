using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTasksLib.App_Code.DomainModel
{
    /// <summary>
    /// Модель сотрудника
    /// </summary>
    [Table("Employee")]
    public class Employee
    {
        public Employee() 
        {
        }

        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public int id_Employee { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$",
            ErrorMessage = "Пароль должен содержать минимум 8 символов, включая буквы и цифры")]
        public string Password { get; set; }

        /// <summary>
        /// Идентификатор соотетствующей модели персоны
        /// </summary>
        public int id_Person { get; set; }

        /// <summary>
        /// Удален или нет
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Дата создания сотрудника
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Ссылка на модель персоны
        /// </summary>
        public virtual Person Person { get; set; }
    }
}


