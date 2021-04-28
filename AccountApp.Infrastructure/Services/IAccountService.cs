using AccountApp.Infrastructure.Dto;

namespace AccountApp.Infrastructure.Services
{
    public interface IAccountService
    {
        AccountDto Get(string email);
        void Register(string email, string password);
    }
}