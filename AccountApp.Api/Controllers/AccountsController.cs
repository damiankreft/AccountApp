using AccountApp.Infrastructure.Commands.Accounts;
using AccountApp.Infrastructure.Dto;
using AccountApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Gets a specific account by email.
        /// </summary>
        /// <param name="email"></param>
        [HttpGet("{email}")]
        public ActionResult<AccountDto> Get(string email)
        {
            var account = _accountService.Get(email);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        /// <summary>
        /// Post new account.
        /// </summary>
        /// <param name="command"></param>
        [HttpPost("")]
        public ActionResult Register([FromBody] CreateAccount command)
        {
            _accountService.Register(command.Email, command.Username, command.Password);
            return Created("http://some.uri", command);
        }
    }
}