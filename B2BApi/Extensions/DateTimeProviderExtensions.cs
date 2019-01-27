using B2BApi.Intrefaces;
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