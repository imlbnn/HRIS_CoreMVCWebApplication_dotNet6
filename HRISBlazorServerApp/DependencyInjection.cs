using AutoMapper;
using Blazored.LocalStorage;
using HRISBlazorServerApp.APISettings;
using HRISBlazorServerApp.Handlers;
using HRISBlazorServerApp.Interfaces;
using HRISBlazorServerApp.Interfaces.Services;
using HRISBlazorServerApp.Mappings;
using HRISBlazorServerApp.Models;
using HRISBlazorServerApp.Providers;
using HRISBlazorServerApp.Services;
using HRISBlazorServerApp.Services.PageServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Radzen;
using System.Configuration;
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
            
            services.AddBlazorAuthentication(Configuration);
            
            services.AddHttpClientConfigForAPIService(Configuration);
            
            services.AddRadzenDependency();

            services.AddScoped<ITokenProviderService, TokenProviderService>();


            //API Services
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDepartmentSectionService, DepartmentSectionService>();
            services.AddScoped<ICivilStatusService, CivilStatusService>();

            return services;
        }

        public static IServiceCollection AddHttpClientConfigForAPIService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<HttpClient>();

            var _serviceAPIOpts = configuration.GetSection(nameof(ServiceAPIOptions)).Get<ServiceAPIOptions>();

            services.AddTransient<NoOpDelegatingHandler>();

            services.AddHttpClient<IEmployeeService, EmployeeService>
                (async (sp, c) =>
                        await ApplyHttpClientOptions(sp, c, _serviceAPIOpts.ApiBaseUrl,
                                    _serviceAPIOpts.RequestTimeout))
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    var handler = GetPrimaryHttpMsgHandler(_serviceAPIOpts);
                    handler.ServerCertificateCustomValidationCallback =
                           (message, cert, chain, errors) => { return true; };
                    return handler;
                })
                //.AddCorrelationIdForwarding() // add the handler to attach the correlation ID to outgoing requests for this named client
                .AddHttpMessageHandler<NoOpDelegatingHandler>();


            services.AddHttpClient<IAccountService, AccountService>
                (async (sp, c) =>
                        await ApplyHttpClientOptions(sp, c, _serviceAPIOpts.ApiBaseUrl,
                                    _serviceAPIOpts.RequestTimeout))
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    var handler = GetPrimaryHttpMsgHandler(_serviceAPIOpts);
                    handler.ServerCertificateCustomValidationCallback =
                           (message, cert, chain, errors) => { return true; };
                    return handler;
                })
                //.AddCorrelationIdForwarding() // add the handler to attach the correlation ID to outgoing requests for this named client
                .AddHttpMessageHandler<NoOpDelegatingHandler>();


            services.AddHttpClient<IDepartmentService, DepartmentService>
                (async (sp, c) =>
                        await ApplyHttpClientOptions(sp, c, _serviceAPIOpts.ApiBaseUrl,
                                    _serviceAPIOpts.RequestTimeout))
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    var handler = GetPrimaryHttpMsgHandler(_serviceAPIOpts);
                    handler.ServerCertificateCustomValidationCallback =
                           (message, cert, chain, errors) => { return true; };
                    return handler;
                })
                //.AddCorrelationIdForwarding() // add the handler to attach the correlation ID to outgoing requests for this named client
                .AddHttpMessageHandler<NoOpDelegatingHandler>();


            services.AddHttpClient<IDepartmentSectionService, DepartmentSectionService>
                (async (sp, c) =>
                        await ApplyHttpClientOptions(sp, c, _serviceAPIOpts.ApiBaseUrl,
                                    _serviceAPIOpts.RequestTimeout))
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    var handler = GetPrimaryHttpMsgHandler(_serviceAPIOpts);
                    handler.ServerCertificateCustomValidationCallback =
                           (message, cert, chain, errors) => { return true; };
                    return handler;
                })
                //.AddCorrelationIdForwarding() // add the handler to attach the correlation ID to outgoing requests for this named client
                .AddHttpMessageHandler<NoOpDelegatingHandler>();


            services.AddHttpClient<ICivilStatusService, CivilStatusService>
                (async (sp, c) =>
                        await ApplyHttpClientOptions(sp, c, _serviceAPIOpts.ApiBaseUrl,
                                    _serviceAPIOpts.RequestTimeout))
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    var handler = GetPrimaryHttpMsgHandler(_serviceAPIOpts);
                    handler.ServerCertificateCustomValidationCallback =
                           (message, cert, chain, errors) => { return true; };
                    return handler;
                })
                //.AddCorrelationIdForwarding() // add the handler to attach the correlation ID to outgoing requests for this named client
                .AddHttpMessageHandler<NoOpDelegatingHandler>();


            return services;
        }

        public static IServiceCollection AddBlazorAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
            });

            services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

            services.AddBlazoredLocalStorage();
            
            services.AddScoped<TokenProvider>();

            services.AddSingleton<TokenConfig>(); 

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

        private static async Task ApplyHttpClientOptions(IServiceProvider sp, HttpClient c,
            string baseUrl, int requestTimeout = 30)
        {
            c.BaseAddress = new Uri(baseUrl);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            c.Timeout = TimeSpan.FromSeconds(requestTimeout);
        }
        private static HttpClientHandler GetPrimaryHttpMsgHandler(ServiceAPIOptions svcOptions)
        {
            var _httpClientHandler = new HttpClientHandler
            {
                UseCookies = false
            };

            if (svcOptions.DisableServerCertificateValidation)
                _httpClientHandler.ServerCertificateCustomValidationCallback
                    = (message, cert, chain, errors) => { return true; };

            return _httpClientHandler;
        }


    }
}
