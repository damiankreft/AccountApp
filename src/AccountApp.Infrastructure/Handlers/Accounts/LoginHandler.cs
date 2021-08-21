using System.Threading.Tasks;
using AccountApp.Infrastructure.Commands;
using AccountApp.Infrastructure.Commands.Accounts;
using AccountApp.Infrastructure.Extensions;
using AccountApp.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;

namespace AccountApp.Infrastructure.Handlers.Accounts
{
    public class LoginHandler : ICommandHandler<Login>
    {
        private readonly IAccountService _accountService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _cache;

        public LoginHandler(IAccountService accountService, IJwtHandler jwtHandler, IMemoryCache cache)
        {
            _accountService = accountService;
            _jwtHandler = jwtHandler;
            _cache = cache;
        }

        public async Task HandleAsync(Login command)
        {
            await _accountService.LoginAsync(command.Email, command.Password);
            var account = await _accountService.GetAsync(command.Email);
            var jwt = _jwtHandler.CreateToken(command.Email, account.Role);
            _cache.SetJwt(command.TokenId, jwt);
        }
    }
}