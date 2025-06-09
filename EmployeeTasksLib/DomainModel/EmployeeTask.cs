using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTasksLib.App_Code.DomainModel
{
    /// <summary>
    /// Модель выполненной задачи сотрудника
    /// </summary>
    [Table("EmployeeTask")]
    public class EmployeeTask
	{
		public EmployeeTask() { }

        /// <summary>
        /// Идентификатор задачи
        /// </summary>        
        public int id_EmployeeTask { get; set; }

        /// <summary>
        /// Идентификатор сотрудника, выполнившего задачу
        /// </summary>
        public int id_Employee { get; set; }

        /// <summary>
        /// Описание задачи
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Время, затраченное на задачу
        /// </summary>
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$",
        ErrorMessage = "Время должно быть в формате чч:мм")]
        public string TimeSpent { get; set; }

        /// <summary>
        /// Дата выполнения задачи
        /// </summary>
        public DateTime DateCompleted { get; set; }

        /// <summary>
        /// Ссылка на модель сотрудника
        /// </summary>
        public virtual Employee Employee { get; set; }
    }
}