using EmployeeTasksLib.App_Code.Application.Base;
using EmployeeTasksLib.App_Code.Presentation.Models;
using EmployeeTasksLib.Presentation.Exceptions;
using EmployeeTasksLib.Presentation.Models;
using System;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeTasksLib.App_Code.Presentation.Services.EmployeeServices
{
    /// <summary>
    /// Сервис по работе с сотрудниками
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class EmployeeService : IEmployeeService
    {
        /// <summary>
        /// Контроллер для работы с сотрудниками
        /// </summary>
        private IEmployeeController _employeeController;

        public EmployeeService(IEmployeeController employeeController)
        {
            _employeeController = employeeController;
        }

        /// <summary>
        /// Метод входа в систему
        /// </summary>
        /// <param name="logindata"></param>
        /// <returns></returns>
        public async Task<WcfResult<LoginOutputDto>> LoginAsync(LoginDto logindata)
        {
            try
            {
                using (var cts = new CancellationTokenSource(TimeSpan.FromMinutes(1))) // timeout 1 минута
                {
                    var result = await _employeeController.CheckLoginAsync(logindata.Email,
                                                                   logindata.Password,
                                                                   cts.Token);
                    return new WcfResult<LoginOutputDto>
                    {
                        Success = true,
                        Data = result
                    };
                }
            }
            catch (Exception ex)
            {
                return ExceptionHelper.CreateComplexErrorResult<LoginOutputDto>
                   (ex, "Ошибка при входе в систему");
            }            
        }

        /// <summary>
        /// Метод регистрации нового сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<WcfResult<string>> RegisterAsync(EmployeeCreateDto employee)
        {
            try
            {
                using (var cts = new CancellationTokenSource(TimeSpan.FromMinutes(1))) // timeout 1 минута
                {
                    await _employeeController.CreateEmployeeAsync(employee, cts.Token);
                    return new WcfResult<string>
                    {
                        Success = true,
                        Data = "Сотрудник успешно зарегистрирован"
                    };
                }
            }
            catch (Exception ex)
            {
                return ExceptionHelper.CreateComplexErrorResult<string>
                   (ex, "Ошибка при регистрации нового сотрудника");
            }            
        }        
    }
}
