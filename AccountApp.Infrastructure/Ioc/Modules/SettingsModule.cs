using AccountApp.Infrastructure.Extensions;
using AccountApp.Infrastructure.Settings;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace AccountApp.Infrastructure.Ioc.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<GeneralSettings>())
                .SingleInstance();
        }
    }
}