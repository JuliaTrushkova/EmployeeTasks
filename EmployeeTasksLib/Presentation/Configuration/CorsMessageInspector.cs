using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System;
using System.Linq;

namespace EmployeeTasksLib.Presentation.Configuration
{
    /// <summary>
    /// Инспектор сообщений для обработки CORS запросов в WCF сервисе
    /// </summary>
    public class CorsMessageInspector : IDispatchMessageInspector
    {
        /// <summary>
        /// Добавление заголовков CORS в ответ на preflight OPTIONS запросы
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <param name="instanceContext"></param>
        /// <returns></returns>
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            // Обработка preflight OPTIONS запросов
            var prop = request.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
            if (prop != null && prop.Method == "OPTIONS")
            {
                var response = Message.CreateMessage(MessageVersion.None, "");
                var responseProperty = new HttpResponseMessageProperty();

                //для тестового всем открываем, но лучше указать конкретные
                responseProperty.Headers.Add("Access-Control-Allow-Origin", "*");
                responseProperty.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                responseProperty.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization, X-Requested-With");
                responseProperty.Headers.Add("Access-Control-Max-Age", "3600");
                responseProperty.StatusCode = HttpStatusCode.OK;
                response.Properties[HttpResponseMessageProperty.Name] = responseProperty;

                // Заменяем исходный запрос на наш ответ
                request = response;

                Console.WriteLine("OPTIONS request handled");
                return "OPTIONS";
            }
            return null;
        }

        /// <summary>
        /// Добавление заголовков CORS в ответ на запросы
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public void BeforeSendReply(ref Message reply, object correlationState)
        {   
            var responseProperty = reply.Properties[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;
            if (responseProperty == null)
            {
                responseProperty = new HttpResponseMessageProperty();
                reply.Properties[HttpResponseMessageProperty.Name] = responseProperty;
            }

            //для тестового всем открываем, но лучше указать конкретные
            if (!responseProperty.Headers.AllKeys.Contains("Access-Control-Allow-Origin"))
            {
                responseProperty.Headers.Add("Access-Control-Allow-Origin", "*");
                responseProperty.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                responseProperty.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization, X-Requested-With");
                responseProperty.StatusCode = HttpStatusCode.OK;
                reply.Properties[HttpResponseMessageProperty.Name] = responseProperty;
                Console.WriteLine($"BeforeSendReply {responseProperty.Headers.ToString()}");
            }
        }
    }
}
