using System;
using AccountApp.Core.Domain;
using AccountApp.Core.Repositories;
using AccountApp.Infrastructure.Dto;
using AutoMapper;

namespace AccountApp.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public void Register(string email, string username, string password)
        {
            var account = _accountRepository.Get(email);

            if (account != null)
            {
                throw new Exception("This email is already used.");
            }

            account = new Account(email, username, password);
            _accountRepository.Add(account);
        }

        AccountDto IAccountService.Get(string email)
        {
            var account = _accountRepository.Get(email);

            if (account is null)
            {
                return null;
            }

            return _mapper.Map<AccountDto>(account);
        }
    }
}