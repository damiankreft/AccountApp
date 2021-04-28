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
    }
}