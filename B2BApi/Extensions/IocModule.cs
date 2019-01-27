using Autofac;
using B2BApi.Intrefaces;
using B2BApi.Providers;
using B2BApi.Repositories;
using B2BApi.Services;

namespace B2BApi.Extensions
{
    public sealed class IocModule : Module
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

            #endregion
            
            #region Services

            builder.RegisterType<AuthorizService>()
                .As<IAuthorizService>()
                .InstancePerLifetimeScope(); 
            
            builder.RegisterType<HandlerService>()
                .As<IHandlerService>()
                .InstancePerLifetimeScope();
            
            #endregion
            
        }
    }
}