using EmployeeTasksLib.Presentation.Models;
using System.Net;
using System.ServiceModel.Web;
using System;
using System.Data.Entity.Validation;

namespace EmployeeTasksLib.Presentation.Exceptions
{
    /// <summary>
    /// Хелпер для отправки сообщений об ошибках
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// Создание сообщения об общего типа ошибке для WCF сервиса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static WcfResult<T> CreateErrorResult<T>(Exception ex, string errorMessage)
            where T : class
        {
            // Логирование ошибки
            Console.WriteLine($"{errorMessage}: {ex.Message}");

            if (WebOperationContext.Current != null)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                WebOperationContext.Current.OutgoingResponse.StatusDescription = $"Error: {ex.Message}";
            }

            return new WcfResult<T>
            {
                Success = false,
                ErrorMessage = $"{errorMessage}: {ex.Message}",
                Data = null
            };
        }

        /// <summary>
        /// Создание сообщения об ошибке WebFaultException для WCF сервиса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="webFault"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static WcfResult<T> CreateWebFaultErrorResult<T>(WebFaultException<T> webFault, string errorMessage)
            where T : class
        {
            // Логирование ошибки
            Console.WriteLine($"{errorMessage}: {webFault.Message} {webFault.Detail}");

            if (WebOperationContext.Current != null)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = webFault.StatusCode;
            }

            // Если это WebFaultException, то возвращаем его сообщение и детали
            return new WcfResult<T>
            {
                Success = false,
                ErrorMessage = $"{errorMessage}: {webFault.Message} {webFault.Detail}",
                Data = null
            };
        }

        /// <summary>
        /// Создание сообщения об ошибке Entity для WCF сервиса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="webFault"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static WcfResult<T> CreateEntityErrorResult<T>(DbEntityValidationException entityEx, string errorMessage)
            where T : class
        {
            var message = string.Empty;

            foreach (var error in entityEx.EntityValidationErrors)
            {
                message = $"Сущность: {error.Entry.Entity.GetType().Name}\n";
                foreach (var err in error.ValidationErrors)
                {
                    message = $"Свойство: {err.PropertyName} Ошибка: {err.ErrorMessage} \n";
                }
            }
            // Логирование ошибки
            Console.WriteLine($"{errorMessage}: {message}");

            if (WebOperationContext.Current != null)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.BadRequest;
            }
            
            return new WcfResult<T>
            {
                Success = false,
                ErrorMessage = $"{errorMessage}: {message}",
                Data = null
            };
        }

        /// <summary>
        /// Создание сообщения об разного типа ошибках для WCF сервиса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static WcfResult<T> CreateComplexErrorResult<T>(Exception ex, string errorMessage)
            where T : class
        {
            if (ex is WebFaultException<T> webFault)
            {
                return CreateWebFaultErrorResult(webFault, errorMessage);
            }

            if (ex is DbEntityValidationException entityEx)
            {
                return CreateEntityErrorResult<T>(entityEx, errorMessage);
            }

            return CreateErrorResult<T>(ex, errorMessage);
        }
    }
}
