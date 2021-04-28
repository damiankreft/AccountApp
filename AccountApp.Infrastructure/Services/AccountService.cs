using System;
using AccountApp.Core.Domain;
using AccountApp.Core.Repositories;
using AccountApp.Infrastructure.Dto;

namespace AccountApp.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Register(string email, string password)
        {
            var account = _accountRepository.Get(email);

            if (account != null)
            {
                throw new Exception("This email is already used.");
            }

            account = new Account(email, password);
            _accountRepository.Add(account);
        }

        AccountDto IAccountService.Get(string email)
        {
            var account = _accountRepository.Get(email);

            if (account is null)
            {
                return null;
            }

            var accountDto = new AccountDto(account.Email, account.PasswordHash);
            return accountDto;
        }
    }
}