using AccountApp.Api;
using AccountApp.Infrastructure.Dto;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace AccountApp.Tests.EndToEnd.Controllers
{
    public class AccountControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public AccountControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

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

        [Fact]
        public async Task returns_jwt_token_when_email_is_valid()
        {
            var json = JsonConvert.SerializeObject( new { email = "test1@example.com", password = "secretPassword" });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var token = await _client.PostAsync($"https://localhost:5001/accounts/login", content);

            var responseContent = await token.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<JwtDto>(responseContent);

            token.IsSuccessStatusCode.Should().BeTrue();

            responseJson.Should().NotBeNull();
            responseJson.Token.Should().NotBeNullOrEmpty();
            responseJson.Expiry.Should().BePositive();
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