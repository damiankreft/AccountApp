using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountApp.Core.Domain;
using AccountApp.Core.Repositories;
using AccountApp.Infrastructure.Dto;
using AutoMapper;
using InvalidCredentialException = System.Security.Authentication.InvalidCredentialException;

namespace AccountApp.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEncrypter _encrypter;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IEncrypter encrypter, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _encrypter = encrypter;
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

        public async Task LoginAsync(string email, string password)
        {
            var account = await _accountRepository.GetAsync(email);

            if (account is null)
            {
                throw new InvalidCredentialException();
            }

            var salt = account.Salt;
            var hash = _encrypter.CreateHash(password, salt);

            if (account.Password != hash)
            {
                throw new InvalidCredentialException();
            }
        }

        public async Task RegisterAsync(string email, string username, string password, string role)
        {
            var account = await _accountRepository.GetAsync(email);

            if (account != null)
            {
                throw new ArgumentException("This email is already used.");
            }

            var salt = _encrypter.CreateSalt(password);
            var hash = _encrypter.CreateHash(password, salt);

            var id = Guid.NewGuid();
            account = new Account(id, email, username, hash, salt, role);
            await _accountRepository.AddAsync(account);
        }
    }
}