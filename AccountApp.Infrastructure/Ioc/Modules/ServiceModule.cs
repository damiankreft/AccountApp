using System.Reflection;
using AccountApp.Infrastructure.Services;
using Autofac;

namespace AccountApp.Infrastructure.Ioc.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var assembly = typeof(CommandModule)
                .GetTypeInfo()
                .Assembly;


            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        
            builder.RegisterType<Encrypter>()
                .As<IEncrypter>()
                .SingleInstance();

            builder.RegisterType<JwtHandler>()
                .As<IJwtHandler>()
                .SingleInstance();
        }
    }
}