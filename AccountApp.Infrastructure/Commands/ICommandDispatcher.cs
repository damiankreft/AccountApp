using System.Threading.Tasks;
using AccountApp.Infrastructure.Commands.Accounts;

namespace AccountApp.Infrastructure.Commands
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}