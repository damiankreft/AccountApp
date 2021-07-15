using AccountApp.Infrastructure.Extensions;
using AccountApp.Infrastructure.Ioc;
using AccountApp.Infrastructure.Settings;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountApp.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                    .AddJsonOptions(options 
                        => options.JsonSerializerOptions.WriteIndented = true);

            var jwtSettings = Configuration.GetSettings<JwtSettings>();
            services.ConfigureJwtAuthentication(jwtSettings);
            services.ConfigureSwagger();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ContainerModule(Configuration));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication()
                .UseDevelopmentConfiguration(env)
                .ConfigureRouting();
        }
    }
}
