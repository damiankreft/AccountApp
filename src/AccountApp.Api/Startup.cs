using AccountApp.Infrastructure.EntityFramework;
using AccountApp.Infrastructure.Extensions;
using AccountApp.Infrastructure.Ioc;
using AccountApp.Infrastructure.Services;
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
                                    builder.WithOrigins(securitySettings.CorsAllowedOrigins)
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowCredentials();
                                });
            }).AddMemoryCache()
                    .AddControllersWithViews()
                    .AddJsonOptions(options 
                        => options.JsonSerializerOptions.WriteIndented = true);
            services.AddJwtAuthentication(jwtSettings, securitySettings)
                    .AddAuthorization()
                    .ConfigureSwagger();

            services.AddEntityFrameworkSqlServer()
                    .AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<AccountContext>();
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

            var generalSettings = Configuration.GetSettings<GeneralSettings>();
            if (generalSettings.SeedData)
            {
                var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
                dataInitializer.SeedAsync();
            }
        }
    }
}
