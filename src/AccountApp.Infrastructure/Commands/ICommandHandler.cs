using System.Threading.Tasks;
using AccountApp.Infrastructure.Commands.Accounts;

namespace AccountApp.Infrastructure.Commands
{
    public interface ICommandHandler<T> where T : ICommand 
    {
        Task HandleAsync(T command);
    }
}