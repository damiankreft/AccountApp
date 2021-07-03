
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountApp.Core.Domain;
using AccountApp.Core.Repositories;
using AccountApp.Infrastructure.Dto;
using AccountApp.Infrastructure.Repositories;
using AccountApp.Infrastructure.Services;
using Moq;
using NUnit.Framework;

namespace AccountApp.Tests.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        private Mock<IAccountRepository> _repository;
        private Mock<AutoMapper.IMapper> _mapper;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IAccountRepository>();
            _mapper = new Mock<AutoMapper.IMapper>();
        }

        [Test]
        public async Task indicates_test_framework_success_when_indicated()
        {
            var accountService = new AccountService(_repository.Object, _mapper.Object);
            await accountService.RegisterAsync("testowyEmail@dot.com", "testowyUser", "testoweHaslo");
            
            _repository.Verify(x => x.AddAsync(It.IsAny<Account>()), Times.Once);
        }

        [Test]
        public async Task gets_all_accounts()
        {
            var accountService = new AccountService(_repository.Object, _mapper.Object);
            await accountService.GetAsync("email@example.com");

            var account = new Account("email@example.com", "username", "passwd");
            _repository.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(() => null);

            _repository.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task returns_account_with_given_email()
        {
            var repositoryMock = _repository;
            var mapperMock = _mapper;

            var accountService = new AccountService(repositoryMock.Object, mapperMock.Object);
            await accountService.GetAsync("email@example.com");

            var account = new Account("email@example.com", "username", "passwd");
            repositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(account);

            repositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
        }
    }
}