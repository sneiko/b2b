using B2BApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace B2BApi.Extensions
{
    public static class ConfigurationsExtensions
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
            => services.AddOptions()
                .Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"))
                .AddSingleton(configuration);    
    }
}