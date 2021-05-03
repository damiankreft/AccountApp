
using System.Threading.Tasks;
using AccountApp.Core.Domain;
using AccountApp.Core.Repositories;
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
    }
}