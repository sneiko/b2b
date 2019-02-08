using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace B2BApi.Extensions
{
    public  static class ContainerBuilderHelper
    {
        public static AutofacServiceProvider RegisterContainer(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<DepModule>();
            var appContainer = builder.Build();
            return new AutofacServiceProvider(appContainer);;
        }
    }
}