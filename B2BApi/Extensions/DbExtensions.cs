using B2BApi.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace B2BApi.Extensions
{
    public static class DbExtensions
    {
        public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
            => services.AddEntityFrameworkSqlite()
                .AddDbContext<B2BDbContext>(options => options
                    .UseSqlite(configuration.GetConnectionString("b2b")));
    }
}