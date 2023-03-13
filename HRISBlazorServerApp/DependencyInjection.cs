using AutoMapper;
using Blazored.LocalStorage;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Mappings;
using HRISBlazorServerApp.Models;
using HRISBlazorServerApp.Providers;
using HRISBlazorServerApp.Services;
using HRISBlazorServerApp.Services.Page;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Radzen;
using System.Net.Http.Headers;

namespace HRISBlazorServerApp
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUIDependency(this IServiceCollection services, IConfiguration Configuration)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApplicationMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<HttpClient>();

            services.AddBlazorAuthentication(Configuration);
            services.AddRadzenDependency();

            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            services.AddBlazoredLocalStorage();
            services.AddScoped<TokenProvider>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }

        public static IServiceCollection AddBlazorAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "/";
                    //option.AccessDeniedPath = "/account/accessdenied";
                    option.ReturnUrlParameter = "/";
                    option.ExpireTimeSpan = TimeSpan.FromDays(1);
                });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
            });

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
