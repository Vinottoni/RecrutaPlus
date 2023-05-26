using Microsoft.Extensions.DependencyInjection;
using RecrutaPlus.Domain.Interfaces;
using RecrutaPlus.Domain.Interfaces.Repositories;
using RecrutaPlus.Domain.Interfaces.Services;
using RecrutaPlus.Domain.Services;
using RecrutaPlus.Infra.Data.Logging;
using RecrutaPlus.Infra.Data.Repositories;

namespace RecrutaPlus.Infra.Data.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Repository
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IOfficeRepository, OfficeRepository>();
            services.AddTransient<IAppLoggerRepository, AppLoggerRepository>();

            //Service
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IOfficeService, OfficeService>();
            services.AddTransient<IAppLoggerService, AppLoggerService>();

            //Logger
            services.AddSingleton<IAppLogger, LoggerAdapter>();
        }

    }
}