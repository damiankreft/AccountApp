using AccountApp.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace AccountApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public abstract class ApiControllerBase : Controller
    {
        protected readonly ICommandDispatcher CommandDispatcher;
        
        protected ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            CommandDispatcher = commandDispatcher;
        }
    }
}