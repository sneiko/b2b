using B2BApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace B2BApi.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var corsConfig = configuration.GetSection("CorsConfiguration").Get<CorsConfiguration>(); 
            services.AddCors(options =>
                options.AddPolicy("CorsPolicy", builder => builder.WithOrigins(corsConfig.Url)
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));
            return services;
        } 
    }
}