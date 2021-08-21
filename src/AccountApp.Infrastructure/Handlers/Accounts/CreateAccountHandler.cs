using System.Threading.Tasks;
using AccountApp.Infrastructure.Commands;
using AccountApp.Infrastructure.Commands.Accounts;
using AccountApp.Infrastructure.Services;

namespace AccountApp.Infrastructure.Handlers.Accounts
{
    public class CreateAccountHandler : ICommandHandler<CreateAccount>
    {
        private readonly IAccountService _accountService;
        
        public CreateAccountHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task HandleAsync(CreateAccount command)
        {
            await _accountService.RegisterAsync(command.Email, command.Username, command.Password, command.Role);
        }
    }
}