using AccountApp.Infrastructure.Commands.Accounts;
using System.Threading.Tasks;

namespace AccountApp.Infrastructure.Commands
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}