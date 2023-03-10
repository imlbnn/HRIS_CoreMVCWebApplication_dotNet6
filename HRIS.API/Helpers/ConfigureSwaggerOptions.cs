using HRIS.API.Filters;
using HRIS.Application.Common.Interfaces.Application;
using HRIS.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HRIS.API
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IConfiguration _configuration;
        private readonly IApp _app;

        public ConfigureSwaggerOptions(IConfiguration configuration, IApp app)
        {
            _configuration = configuration;
            _app = app;
        }

        public void Configure(SwaggerGenOptions options)
        {
            //Add Swagger Document
            options.SwaggerDoc("AUTH", new OpenApiInfo
            {
                Title = "Identity Server Environment",
                Version = "1",
                Description = "Rest APIs for Identity Server"
            });

            options.SwaggerDoc("HRIS", new OpenApiInfo
            {
                Title = "HRIS Environment",
                Version = "1",
                Description = "Rest APIs for HRIS Application"
            });


            //Add Swagger Security
            options.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme.ToLowerInvariant(),
                In = ParameterLocation.Header,
                Name = "Authorization",
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme."
            });

            options.OperationFilter<AuthResponsesOperationFilter>();

        }
    }
}
