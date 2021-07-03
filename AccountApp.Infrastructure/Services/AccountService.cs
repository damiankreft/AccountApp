using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<List<AccountDto>> GetAllAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();

            return accounts is null ? new List<AccountDto>() : _mapper.Map<List<AccountDto>>(accounts);
        }

        public async Task<AccountDto> GetAsync(string email)
        {
            var account = await _accountRepository.GetAsync(email);

            if (account is null)
            {
                return null;
            }

            return _mapper.Map<AccountDto>(account);
        }

        public async Task RegisterAsync(string email, string username, string password)
        {
            var account = await _accountRepository.GetAsync(email);

            if (account != null)
            {
                throw new ArgumentException("This email is already used.");
            }

            account = new Account(email, username, password);
            await _accountRepository.AddAsync(account);
        }
    }
}