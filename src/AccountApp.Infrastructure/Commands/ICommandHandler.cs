using AccountApp.Infrastructure.Commands.Accounts;
using System.Threading.Tasks;

namespace AccountApp.Infrastructure.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}