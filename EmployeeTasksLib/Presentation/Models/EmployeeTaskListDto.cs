using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EmployeeTasksLib.App_Code.Presentation.Models
{
    /// <summary>
    /// Модель списка выполненных сотрудниками задач
    /// </summary>
    [DataContract]
    public class EmployeeTaskListDto
	{
		public EmployeeTaskListDto()
		{
		}

        /// <summary>
        /// Список выполненных сотрудниками задач
        /// </summary>
        [DataMember]
        public List<EmployeeTaskListItemDto> EmployeeTasks { get; set; }

        /// <summary>
        /// Общее время, затраченное на выполнение задач
        /// </summary>
        [DataMember]
        public string TotalTimeSpent { get; set; }
    }
}