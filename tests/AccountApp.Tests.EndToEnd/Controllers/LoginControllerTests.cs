using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AccountApp.Infrastructure.Dto;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace AccountApp.Tests.EndToEnd.Controllers
{
    [Collection("ControllerTests")]
    public class LoginControllerTests
    {
        private readonly ControllerTestsFixture _fixture;
        public LoginControllerTests(ControllerTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task returns_jwt_token_when_email_is_valid()
        {
            var json = JsonConvert.SerializeObject(new { email = "user1@example.com", password = "secretPassword" });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var token = await _fixture.GetClient().PostAsync($"https://localhost:5001/login", content);

            var responseContent = await token.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<JwtDto>(responseContent);

            token.IsSuccessStatusCode.Should().BeTrue();

            responseJson.Should().NotBeNull();
            responseJson.Token.Should().NotBeNullOrEmpty();
            responseJson.Expiry.Should().BePositive();
        }
    }
}