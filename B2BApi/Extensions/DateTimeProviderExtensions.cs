using B2BApi.Interfaces;
using B2BApi.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace B2BApi.Extensions
{
    public static class DateTimeProviderExtensions
    {
        public static void AddDateTimeProvider(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        }
    }
}