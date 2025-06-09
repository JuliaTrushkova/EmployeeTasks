using System.Runtime.Serialization;

namespace EmployeeTasksLib.App_Code.Presentation.Models
{
    /// <summary>
    /// Модель строки списка выполненных сотрудниками задач
    /// </summary>    
    [DataContract]
    public class EmployeeTaskListItemDto
    {
		public EmployeeTaskListItemDto() { }

        /// <summary>
        /// Идентификатор задачи
        /// </summary>        
        [DataMember]
        public int id_EmployeeTask { get; set; }

        /// <summary>
        /// ФИО сотрудника, выполнившего задачу
        /// </summary>        
        [DataMember]
        public string FIO_Employee { get; set; }

        /// <summary>
        /// Описание задачи
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Время, затраченное на задачу
        /// </summary>
        [DataMember]
        public string TimeSpent { get; set; }

        /// <summary>
        /// Дата выполнения задачи
        /// </summary>
        [DataMember]
        public string DateCompleted { get; set; }
    }
}