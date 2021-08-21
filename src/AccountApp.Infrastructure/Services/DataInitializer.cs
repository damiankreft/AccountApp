using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AccountApp.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<DataInitializer> _logger;
        public DataInitializer(IAccountService accountService, ILogger<DataInitializer> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }
        public async Task SeedAsync()
        {
            _logger.Log(LogLevel.Trace, "Begin data initialization.");
            var tasks = new List<Task>();
            
            for (var i = 0; i < 10; i++)
            {
                tasks.Add(_accountService.RegisterAsync($"user{i}@example.com", $"user{i}", "secretPassword", "user"));
            }

            for (var i = 0; i < 3; i++)
            {
                tasks.Add(_accountService.RegisterAsync($"admin{i}@example.com", $"admin{i}", "secretPassword", "admin"));
            }

            await Task.WhenAll(tasks);
            _logger.Log(LogLevel.Trace, "Data initialization has finished successfully.");
        }
    }
}