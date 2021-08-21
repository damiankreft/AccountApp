using AccountApp.Infrastructure.Extensions;
using AccountApp.Infrastructure.Ioc;
using AccountApp.Infrastructure.Settings;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountApp.Api
{
    public class Startup
    {
        readonly string AllowedSpecificOrigins = "_allowedSpecificOrigins";

        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var jwtSettings = Configuration.GetSettings<JwtSettings>();
            var securitySettings = Configuration.GetSettings<SecuritySettings>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowedSpecificOrigins,
                                builder =>
                                {
                                    builder.WithOrigins(securitySettings.CorsAllowedOrigins);
                                });
            }).AddMemoryCache()
                    .AddControllersWithViews()
                    .AddJsonOptions(options 
                        => options.JsonSerializerOptions.WriteIndented = true);
            services.ConfigureJwtAuthentication(jwtSettings, securitySettings)
                    .AddAuthorization()
                    .ConfigureSwagger();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ContainerModule(Configuration));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(AllowedSpecificOrigins)
                .UseAuthentication()
                .UseDevelopmentConfiguration(env)
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>  
                    {
                        endpoints.MapGet("/", async context => { 
                            await context.Response.WriteAsync("Hello, world!");
                        });
                        endpoints.MapControllers();
                    });
        }
    }
}
