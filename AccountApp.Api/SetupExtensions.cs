using System;
using System.IO;
using System.Reflection;
using System.Text;
using AccountApp.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace AccountApp.Api
{
    public static class SetupExtensions
    {
        /// <summary>
        /// Adds developer exception page and swagger.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IApplicationBuilder UseDevelopmentConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(config => 
                {
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger test");
                });
            }

            return app;
        }

        /// <summary>
        /// Adds developer exception page and swagger.
        /// </summary>
        /// <param name="app"></param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IApplicationBuilder ConfigureRouting(this IApplicationBuilder app)
            =>  app.UseRouting()
                    .UseEndpoints(endpoints =>  
                    {
                        endpoints.MapGet("/", async context => { 
                            await context.Response.WriteAsync("Hello, world!");
                        });
                        endpoints.MapControllers();
                    });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="jwtSettings"></param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        {
            var encodedKey = Encoding.UTF8.GetBytes(jwtSettings.Key);
            services.AddAuthentication(options => 
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(encodedKey)
                };
            });

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
            => services.AddSwaggerGen(config => 
                {
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    config.IncludeXmlComments(xmlPath);
                });
    }
}