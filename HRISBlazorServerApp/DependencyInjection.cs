using HRIS.Application.Common.Interfaces.Services;
using HRISBlazorServerApp.Data;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Services;
using HRISBlazorServerApp.Services.Page;

namespace HRISBlazorServerApp
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUIDependency(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton<WeatherForecastService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}
