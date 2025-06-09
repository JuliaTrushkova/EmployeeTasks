using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace EmployeeTasksLib.App_Code.Presentation.Models
{
    /// <summary>
    /// Модель при авторизации сотрудника
    /// </summary>
    [DataContract]
    public class LoginDto
    {
        public LoginDto() 
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
    }
}


