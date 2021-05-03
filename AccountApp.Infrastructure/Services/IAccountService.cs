using System.Collections.Generic;
using System.Threading.Tasks;
using AccountApp.Infrastructure.Dto;

namespace AccountApp.Infrastructure.Services
{
    public interface IAccountService
    {
        Task<List<AccountDto>> GetAllAsync();
        Task<AccountDto> GetAsync(string email);
        Task RegisterAsync(string email, string username, string password);
    }
}