using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace B2BApi.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
            =>  services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Quion API",
                    Description = "API для обработки прайсов",
                    Contact = new OpenApiContact()
                    {
                        Name = "F Developers",
                        Email = "036006@gmail.com"
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Use under GNU v.3",
                        Url = new Uri("https://www.gnu.org/licenses/gpl-3.0.ru.html")
                    }
                });
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT авторизация: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                
                var security = new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme() { BearerFormat = "Bearer" }, 
                        new string[] { }
                    },
                };
                c.AddSecurityRequirement(security);
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.DescribeAllEnumsAsStrings();
                c.DescribeAllParametersInCamelCase();
            });
        
        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
            => app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quion API V1");
                    c.RoutePrefix = string.Empty;
                });
    }
}