using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace EmployeeTasksLib.App_Code.Presentation.Models
{
    /// <summary>
    /// Модель создания сотрудника
    /// </summary>
    [DataContract]
    public class EmployeeCreateDto
    {
        public EmployeeCreateDto() 
        {            
        }

        /// <summary>
        /// Электронная почта
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "Почта обязательна")]
        [StringLength(100, ErrorMessage = "Почта должна быть до 100 символов")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [DataMember]
        [Required(ErrorMessage = "Пароль обязателен")]
        [StringLength(100, ErrorMessage = "Пароль должен быть до 100 символов")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$",
            ErrorMessage = "Пароль должен содержать минимум 8 символов, включая буквы и цифры")]
        public string Password { get; set; }        

        [DataMember]
        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(150, ErrorMessage = "Имя должно быть до 150 символов")]
        public string FirstName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Фамилия обязательна")]
        [StringLength(150, ErrorMessage = "Фамилия должна быть до 150 символов")]
        public string LastName { get; set; }

    }
}


