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
        public AccountDto Get(string email)
            => _accountService.Get(email);
    }
}