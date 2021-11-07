using AccountApp.Infrastructure.Commands;
using AccountApp.Infrastructure.Commands.Accounts;
using AccountApp.Infrastructure.Dto;
using AccountApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountApp.Api.Controllers
{
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
        [Authorize]
        public async Task<ActionResult<List<AccountDto>>> GetAll()
        {
            var accounts = await _accountService.GetAllAsync();

            return accounts is null ? NotFound() : accounts;
        }

        /// <summary>
        /// Gets a specific account by email.
        /// </summary>
        /// <param name="email"></param>
        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var account = await _accountService.GetAsync(email);

            return account is null ? NotFound() : Json(account);
        }

        /// <summary>
        /// Create a new account.
        /// </summary>
        /// <param name="command"></param>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateAccount command)
        {
            try
            {
                await CommandDispatcher.DispatchAsync(command);

                return Created($@"accounts/{command.Email}", command);
            }
            catch (System.ArgumentException)
            {
                return Conflict("This email is used already.");
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}