using EmployeeTasksLib.App_Code.Presentation.Models;
using EmployeeTasksLib.Presentation.Models;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace EmployeeTasksLib.App_Code.Presentation.Services.EmployeeServices
{
    /// <summary>
    /// Контракт для сервиса по работе с сотрудниками
    /// </summary>
    [ServiceContract]
    public interface IEmployeeService
    {
        /// <summary>
        /// Метод входа в систему
        /// </summary>
        /// <param name="logindata"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json)]
        Task<WcfResult<LoginOutputDto>> LoginAsync(LoginDto logindata);

        /// <summary>
        /// Метод регистрации нового сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json)]
        Task<WcfResult<string>> RegisterAsync(EmployeeCreateDto employee);        
    }
}


