using Autofac;
using B2BApi.Interfaces;
using B2BApi.Providers;
using B2BApi.Repositories;
using B2BApi.Services;

namespace B2BApi.Extensions
{
    public sealed class DepModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {            
            
            #region Infrostructure
            
            builder.RegisterType<DateTimeProvider>()
                .As<IDateTimeProvider>()
                .SingleInstance();
            
            builder.RegisterType<Sha256HashProvider>()
                .As<IHashProvider>()
                .InstancePerLifetimeScope();
            
            #endregion

            #region Repositories

            builder.RegisterType<UsersRepository>()
                .As<IUsersRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<HandlerRepository>()
                .As<IHandlerRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<ProductRepository>()
                .As<IProductRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<StockRepository>()
                .As<IStockRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<BrandRepository>()
                .As<IBrandRepository>()
                .InstancePerLifetimeScope();

            #endregion
            
            #region Services

            builder.RegisterType<AuthorizService>()
                .As<IAuthorizService>()
                .InstancePerLifetimeScope(); 
            
            builder.RegisterType<HandlerService>()
                .As<IHandlerService>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<ProductService>()
                .As<IProductService>()
                .InstancePerLifetimeScope();
            
            #endregion
            
        }
    }
}