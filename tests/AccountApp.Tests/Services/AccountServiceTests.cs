using System;
using System.Threading.Tasks;
using AccountApp.Core.Domain;
using AccountApp.Core.Repositories;
using AccountApp.Infrastructure.Repositories;
using AccountApp.Infrastructure.Services;
using Moq;
using Xunit;

namespace AccountApp.Tests.Services
{
    public class AccountServiceTests
    {
        private Mock<IAccountRepository> _repository;
        private IAccountRepository _repo;
        private Mock<IEncrypter> _encrypter;
        private Mock<AutoMapper.IMapper> _mapper;

        public AccountServiceTests()
        {
            _repository = new Mock<IAccountRepository>();
            _repo = new InMemoryAccountRepository();
            _encrypter = new Mock<IEncrypter>();
            _mapper = new Mock<AutoMapper.IMapper>();
        }

        [Fact]
        public async Task calls_register_account_once()
        {
            var accountService = new AccountService(_repository.Object, _encrypter.Object, _mapper.Object);
            await accountService.RegisterAsync("testowyEmail@dot.com", "testowyUser", "testoweHaslo", "user");
            
            _repository.Verify(x => x.AddAsync(It.IsAny<Account>()), Times.Once);
        }

        [Fact]
        public async Task gets_all_accounts()
        {
            var accountService = new AccountService(_repository.Object, _encrypter.Object, _mapper.Object);
            await accountService.GetAsync("email@example.com");

            var account = new Account(Guid.NewGuid(), "email@example.com", "username", "passwd", "someSalt", "user");
            _repository.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(() => null);

            _repository.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task returns_account_with_given_email()
        {
            var repositoryMock = _repository;
            var mapperMock = _mapper;

            var accountService = new AccountService(repositoryMock.Object, _encrypter.Object, mapperMock.Object);
            await accountService.GetAsync("email@example.com");

            var account = new Account(Guid.NewGuid(), "email@example.com", "username", "passwd", "someSalt", "user");
            repositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(account);

            repositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
        }
    }
}