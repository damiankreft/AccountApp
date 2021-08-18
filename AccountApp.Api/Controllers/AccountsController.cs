using System.Collections.Generic;
using System.Threading.Tasks;
using AccountApp.Infrastructure.Commands;
using AccountApp.Infrastructure.Commands.Accounts;
using AccountApp.Infrastructure.Dto;
using AccountApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountApp.Api.Controllers
{
    public class AccountsController : ApiControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJwtHandler _jwtHandler;

        public AccountsController(IAccountService accountService, IJwtHandler jwtHandler, ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _accountService = accountService;
            _jwtHandler = jwtHandler;
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

        public async Task<IActionResult> GetToken(string email, string role)
        {
            var token = await Task.FromResult(_jwtHandler.CreateToken(email, role));

            return Json(token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]Login command)
        {
            try
            {
                await _accountService.LoginAsync(command.Email, command.Password);
                var account = await _accountService.GetAsync(command.Email);
                
                return await GetToken(command.Email, account.Role);
            }
            catch (System.Security.Authentication.InvalidCredentialException)
            {
                return Content("Do not steal!");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
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
                await _commandDispatcher.DispatchAsync(command);
            
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