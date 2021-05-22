using AccountApp.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace AccountApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public abstract class ApiControllerBase : Controller
    {
        protected readonly ICommandDispatcher _commandDispatcher;
        
        protected ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }
    }
}