using System;
using System.Threading.Tasks;
using AccountApp.Infrastructure.Commands.Accounts;
using Autofac;

namespace AccountApp.Infrastructure.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        public readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }


        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command), 
                $"Command {typeof(T).Name} cannot be null.");
            }

            var handler = _context.Resolve<ICommandHandler<T>>();
            await handler.HandleAsync(command);
        }
    }
}