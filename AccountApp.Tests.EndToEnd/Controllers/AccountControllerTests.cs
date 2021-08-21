using AccountApp.Api;
using AccountApp.Infrastructure.Dto;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace AccountApp.Tests.EndToEnd.Controllers
{
    public class AccountControllerTests : ControllerTestBase
    {
        public AccountControllerTests(WebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async Task returns_account_assigned_to_given_email()
        {
            var email = "test1@example.com";

            var account = await GetAccountAsync(email);

            account.Email.Should().BeEquivalentTo(email);
            
        }

        [Fact]
        public async Task returns_404_when_given_email_is_invalid()
        {
            var email = "invalidAccount123321123321@example.com";
            var response = await _client.GetAsync($"accounts/{email}");
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task creates_account_when_given_email_is_valid()
        {
             var email = "validUser@example.com";
             var username = "validUser";
             var passwordHash = "D9F7165457A43834AAD524F80717553E0B73CF7E79F40BEBDC316B7A2B26FF7B";
             var account = new { Email = email, Username = username, Password = passwordHash };
             var payload = GetHttpPayload(account);
             
             var response = await _client.PostAsync("accounts", payload);

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
            var response = await _client.GetAsync($"accounts/{email}");
            var message = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<AccountDto>(message);
        }
    }
}