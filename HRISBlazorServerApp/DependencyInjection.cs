using Blazored.LocalStorage;
using HRIS.Application.Common.Interfaces.Services;
using HRIS.Infrastructure;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Models;
using HRISBlazorServerApp.Providers;
using HRISBlazorServerApp.Services;
using HRISBlazorServerApp.Services.Page;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;

namespace HRISBlazorServerApp
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUIDependency(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddBlazorAuthentication(Configuration);

            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            services.AddBlazoredLocalStorage();
            services.AddScoped<TokenProvider>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }

        public static IServiceCollection AddRadzenDependency(this IServiceCollection services)
        {
            services.AddScoped<ContextMenuService>();
            services.AddScoped<DialogService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<NotificationService>();

            return services;
        }



           
    }
}
