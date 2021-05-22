using System.Collections.Generic;
using System.Threading.Tasks;
using AccountApp.Infrastructure.Commands;
using AccountApp.Infrastructure.Commands.Accounts;
using AccountApp.Infrastructure.Dto;
using AccountApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ApiControllerBase
    {
        private readonly IAccountService _accountService;
        
        public AccountsController(IAccountService accountService, ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Gets all accounts
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<AccountDto>>> GetAll()
        {
            var accounts = await _accountService.GetAllAsync();
            if (accounts is null)
            {
                return NotFound();
            }

            return accounts;
        }

        /// <summary>
        /// Gets a specific account by email.
        /// </summary>
        /// <param name="email"></param>
        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var account = await _accountService.GetAsync(email);

            if (account == null)
            {
                return NotFound();
            }

            return Json(account);
        }

        /// <summary>
        /// Post new account.
        /// </summary>
        /// <param name="command"></param>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateAccount command)
        {
            await _commandDispatcher.DispatchAsync(command);
            
            return Created($@"accounts/{command.Email}", command);
        }
    }
}