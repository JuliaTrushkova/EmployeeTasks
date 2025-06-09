using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace EmployeeTasksLib.App_Code.Presentation.Models
{
    /// <summary>
    /// Модель создания выполненной задачи сотрудника
    /// </summary>    
    [DataContract]
    public class EmployeeTaskCreateDto
    {
		public EmployeeTaskCreateDto() { }

        /// <summary>
        /// Идентификатор сотрудника, выполнившего задачу
        /// </summary>        
        [DataMember]
        [Required(ErrorMessage = "Идентификатор сотрудника обязателен")]
        public int id_Employee { get; set; }

        /// <summary>
        /// Дата завершения задачи
        /// </summary>        
        [DataMember]
        [Required(ErrorMessage = "Дата завершения задачи обязательна")]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[0-2])\.(\d{4})$",
            ErrorMessage = "Дата должна быть в формате ДД.ММ.ГГГГ")]
        public string DateCompleted { get; set; }

        /// <summary>
        /// Описание задачи
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "Описание обязательно")]
        [StringLength(500, ErrorMessage = "Описание должно быть до 500 символов")]
        public string Description { get; set; }

        /// <summary>
        /// Время, затраченное на задачу
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "Время, затраченнное на выполнение задачи, обязательно")]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$",
            ErrorMessage = "Время должно быть в формате чч:мм")]
        public string TimeSpent { get; set; }
    }
}