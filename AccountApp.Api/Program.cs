using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Autofac.Extensions.DependencyInjection;

namespace AccountApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webHostBuilder => 
                    {
                        webHostBuilder
                            .UseStartup<Startup>()
                            .UseContentRoot(Directory.GetCurrentDirectory());
                            
                    })
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
