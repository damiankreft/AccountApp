using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                var user = new
                {
                    Email = $"user{i}@example.com",
                    Username = $"user{i}",
                    Password = "secretPassword",
                    Role = "user",
                    LastAttemptIp = "189.47.211.214"
                };
                await _accountService.RegisterAsync(user.Email, user.Username, user.Password, user.Role);
                _logger.Log(LogLevel.Trace, $"Created a new user={user.Username} with role={user.Role}");
            }

            for (var i = 0; i < 3; i++)
            {
                var user = new
                {
                    Email = $"admin{i}@example.com",
                    Username = $"admin{i}",
                    Password = "secretPassword",
                    Role = "admin",
                    LastAttemptIp = "127.0.0.1"
                };
                await _accountService.RegisterAsync(user.Email, user.Username, user.Password, user.Role);
                _logger.Log(LogLevel.Trace, $"Created a new user={user.Username} with role={user.Role}");
            }

            _logger.Log(LogLevel.Trace, "Data initialization has finished successfully.");
        }
    }
}