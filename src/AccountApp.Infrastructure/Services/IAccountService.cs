using AccountApp.Infrastructure.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountApp.Infrastructure.Services
{
    public interface IAccountService : IService
    {
        Task<List<AccountDto>> GetAllAsync();
        Task<AccountDto> GetAsync(string email);
        Task RegisterAsync(string email, string username, string password, string role);
        Task LoginAsync(string email, string password);
    }
}