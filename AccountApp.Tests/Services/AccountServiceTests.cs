
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
        [Test]
        public async Task indicates_test_framework_success_when_indicated()
        {
            var repository = new Mock<IAccountRepository>();
            var mapper = new Mock<AutoMapper.IMapper>();

            var accountService = new AccountService(repository.Object, mapper.Object);
            await accountService.RegisterAsync("testowyEmail@dot.com", "testowyUser", "testoweHaslo");
            
            repository.Verify(x => x.AddAsync(It.IsAny<Account>()), Times.Once);
        }

        [Test]
        public async Task returns_account_with_given_email_if_account_exists()
        {
            var repository = new InMemoryAccountRepository();
            var mapper = new Mock<AutoMapper.IMapper>();
            var accountService = new AccountService(repository, mapper.Object);

            var account = await accountService.GetAsync("test1@example.com");
            Assert.IsInstanceOf(typeof(AccountDto), account);
        }

        [Test]
        public async Task gets_all_accounts()
        {
            var repositoryMock = new Mock<IAccountRepository>();
            var mapperMock = new Mock<AutoMapper.IMapper>();

            var accountService = new AccountService(repositoryMock.Object, mapperMock.Object);
            await accountService.GetAsync("email@example.com");

            var account = new Account("email@example.com", "username", "passwd");
            repositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(() => null);

            repositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task returns()
        {
            var repositoryMock = new Mock<IAccountRepository>();
            var mapperMock = new Mock<AutoMapper.IMapper>();

            var accountService = new AccountService(repositoryMock.Object, mapperMock.Object);
            await accountService.GetAsync("email@example.com");

            var account = new Account("email@example.com", "username", "passwd");
            repositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(account);

            repositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
        }
    }
}