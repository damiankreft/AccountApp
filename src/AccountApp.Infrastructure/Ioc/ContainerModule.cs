using AccountApp.Infrastructure.Ioc.Modules;
using AccountApp.Infrastructure.Mappers;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace AccountApp.Infrastructure.Ioc
{
    public class ContainerModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new CommandModule());
            builder.RegisterModule(new SettingsModule(_configuration));
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<SqlModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterInstance(AutoMapperConfig.Initialize())
                .SingleInstance();
        }
    }
}