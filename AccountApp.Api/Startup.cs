using System;
using System.IO;
using System.Reflection;
using AccountApp.Core.Repositories;
using AccountApp.Infrastructure.Ioc.Modules;
using AccountApp.Infrastructure.Mappers;
using AccountApp.Infrastructure.Repositories;
using AccountApp.Infrastructure.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AccountApp.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            
            services.AddSwaggerGen(config => 
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
            builder.RegisterModule(new CommandModule());
            builder.RegisterType<InMemoryAccountRepository>().As<IAccountRepository>().InstancePerLifetimeScope();
            builder.RegisterInstance(AutoMapperConfig.Initialize()).SingleInstance();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(config => 
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger test");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>  
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
