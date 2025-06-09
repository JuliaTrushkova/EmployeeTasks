using Autofac;
using Autofac.Integration.Wcf;
using EmployeeTasksLib.App_Code.Application;
using EmployeeTasksLib.App_Code.Application.Base;
using EmployeeTasksLib.App_Code.Infrastructure;
using EmployeeTasksLib.App_Code.Infrastructure.Interface;
using EmployeeTasksLib.App_Code.Presentation.Services.EmployeeServices;
using EmployeeTasksLib.App_Code.Presentation.Services.EmployeeTasksServices;
using EmployeeTasksLib.Presentation.Configuration;
using System;
using System.ServiceModel;

namespace EmployeeTasksHost
{
    class Program
    {
        private static IContainer _container;
        private static ServiceHost _employeeHost;
        private static ServiceHost _employeeTaskHost;

        static void Main(string[] args)
        {
            Console.WriteLine("Start host");
            try
            {
                ConfigureBuilder();

                StartHosts();

                Console.WriteLine("Нажмите любую клавишу для остановки сервисов...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while starting host: {ex.Message}");
            }
            finally
            {
                StopHost();
            }
        }

        /// <summary>
        /// Конфигурируем Autofac (для DI)
        /// </summary>
        private static void ConfigureBuilder()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<EmployeeTasksDbContext>().As<IEmployeeTasksDbContext>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<EmployeeTaskController>().As<IEmployeeTaskController>();
            containerBuilder.RegisterType<EmployeeTasksService>().As<IEmployeeTasksService>();

            containerBuilder.RegisterType<EmployeeController>().As<IEmployeeController>();
            containerBuilder.RegisterType<EmployeeService>().As<IEmployeeService>();

            _container = containerBuilder.Build();
        }

        /// <summary>
        /// Запускаем сервисы
        /// </summary>
        private static void StartHosts()
        {
            StartOneHost<IEmployeeService, EmployeeService>(_employeeHost, "EmployeeService");
            StartOneHost<IEmployeeTasksService, EmployeeTasksService>(_employeeTaskHost, "EmployeeTasksService");     
        }

        private static void StartOneHost<T,Y>(ServiceHost employeeHost, string name)
            where T : class
            where Y : T
        {
            employeeHost = new ServiceHost(typeof(Y));

            // Добавляем CORS поведение
            employeeHost.Description.Behaviors.Add(new CorsBehavior());
            // Подключаем Autofac к WCF
            employeeHost.AddDependencyInjectionBehavior<T>(_container);
            employeeHost.Open();
            Console.WriteLine($"Сервис {name} запущен.");
        }

        /// <summary>
        /// Останавливаем сервисы
        /// </summary>
        private static void StopHost()
        {
            try
            {
                if (_employeeHost != null)
                {
                    if (_employeeHost.State == CommunicationState.Opened)
                    {
                        _employeeHost.Close();
                    }
                    else if (_employeeHost.State == CommunicationState.Faulted)
                    {
                        _employeeHost.Abort();
                    }
                    _employeeHost = null;
                    Console.WriteLine("Сервис EmployeeService остановлен.");
                }

                if (_employeeTaskHost != null)
                {
                    if (_employeeTaskHost.State == CommunicationState.Opened)
                    {
                        _employeeTaskHost.Close();
                    }
                    else if (_employeeTaskHost.State == CommunicationState.Faulted)
                    {
                        _employeeTaskHost.Abort();
                    }
                    _employeeTaskHost = null;
                    Console.WriteLine("Сервис EmployeeTasksService остановлен.");
                }

                if (_container != null)
                {
                    _container.Dispose();
                    _container = null;
                    Console.WriteLine("Container освобожден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при остановке сервисов: {ex.Message}");
            }
        }
    }
}
