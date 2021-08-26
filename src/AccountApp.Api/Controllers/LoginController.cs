using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using AccountApp.Infrastructure.Commands;
using AccountApp.Infrastructure.Commands.Accounts;
using AccountApp.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AccountApp.Api.Controllers
{
    public class LoginController : ApiControllerBase
    {
        private readonly IMemoryCache _cache;
        public LoginController(ICommandDispatcher commandDispatcher, IMemoryCache cache) : base(commandDispatcher)
        {
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]Login command)
        {
            try
            {
                command.TokenId = Guid.NewGuid();
                await CommandDispatcher.DispatchAsync(command);
                var jwt = _cache.GetJwt(command.TokenId);

                return Json(jwt);
            }
            catch (InvalidCredentialException)
            {
                return Unauthorized("Wrong email or password. Try again.");
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}