using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AccountApp.Api;
using AccountApp.Infrastructure.Dto;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace AccountApp.Tests.EndToEnd.Controllers
{
    public class LoginControllerTests : ControllerTestBase
    {
        public LoginControllerTests(WebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async Task returns_jwt_token_when_email_is_valid()
        {
            var json = JsonConvert.SerializeObject( new { email = "test1@example.com", password = "secretPassword" });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var token = await _client.PostAsync($"https://localhost:5001/login", content);

            var responseContent = await token.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<JwtDto>(responseContent);

            token.IsSuccessStatusCode.Should().BeTrue();

            responseJson.Should().NotBeNull();
            responseJson.Token.Should().NotBeNullOrEmpty();
            responseJson.Expiry.Should().BePositive();
        }
    }
}