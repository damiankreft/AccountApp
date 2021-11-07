using AccountApp.Infrastructure.Dto;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AccountApp.Tests.EndToEnd.Controllers
{
    [Collection("ControllerTests")]
    public class AccountControllerTests
    {
        private readonly ControllerTestsFixture _fixture;
        public AccountControllerTests(ControllerTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task returns_account_assigned_to_given_email()
        {
            var email = "user1@example.com";

            var account = await GetAccountAsync(email);

            account.Email.Should().BeEquivalentTo(email);
        }

        [Fact]
        public async Task returns_404_when_given_email_is_invalid()
        {
            var email = "invalidAccount123321123321@example.com";
            var response = await _fixture.GetClient().GetAsync($"accounts/{email}");
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task creates_account_when_given_email_is_valid()
        {
            var email = "validUser@example.com";
            var username = "validUser";
            var password = "D9F7165457A43834AAD524F80717553E0B73CF7E79F40BEBDC316B7A2B26FF7B";
            var account = new { Email = email, Username = username, Password = password };
            var payload = GetHttpPayload(account);

            var response = await _fixture.GetClient().PostAsync("accounts", payload);

            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ToString().Should().BeEquivalentTo($"accounts/{account.Email}");
        }

        private static StringContent GetHttpPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            var contentType = "application/json";

            return new StringContent(json, Encoding.UTF8, contentType);
        }

        private async Task<AccountDto> GetAccountAsync(string email)
        {
            var response = await _fixture.GetClient().GetAsync($"accounts/{email}");
            var message = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AccountDto>(message);
        }
    }
}