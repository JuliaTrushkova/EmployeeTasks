using EmployeeTasksLib.App_Code.Presentation.Models;
using EmployeeTasksLib.Presentation.Models;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace EmployeeTasksLib.App_Code.Presentation.Services.EmployeeTasksServices
{
	/// <summary>
	/// Контракт для сервиса по работе с задачами сотрудников
	/// </summary>
	[ServiceContract]
	public interface IEmployeeTasksService
	{
		/// <summary>
		/// Метод получения списка задач
		/// </summary>
		/// <returns></returns>
		[OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        Task<WcfResult<EmployeeTaskListDto>> GetListAsync();

		/// <summary>
		/// Метод создания новой задачи
		/// </summary>
		/// <param name="employeeTask"></param>
		/// <returns></returns>
		[OperationContract]
        [WebInvoke(Method = "POST",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json)]
        Task<WcfResult<string>> CreateNewTaskAsync(EmployeeTaskCreateDto employeeTask);
	}
}


