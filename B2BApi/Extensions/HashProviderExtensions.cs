using B2BApi.Intrefaces;
using B2BApi.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace B2BApi.Extensions
{
    public static class HashProviderExtensions
    {
        public static void AddSha256Hasher(this IServiceCollection services)
        {
            services.AddSingleton<IHashProvider, Sha256HashProvider>();
        }
    }
}