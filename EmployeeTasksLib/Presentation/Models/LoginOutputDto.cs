using System.Runtime.Serialization;

namespace EmployeeTasksLib.App_Code.Presentation.Models
{
    /// <summary>
    /// Модель ответа при авторизации сотрудника
    /// </summary>
    [DataContract]
    public class LoginOutputDto
    {
        public LoginOutputDto() 
        {            
        }

        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>        
        [DataMember]
        public int id_Employee { get; set; }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>        
        [DataMember]
        public string FIO_Employee { get; set; }
    }
}


