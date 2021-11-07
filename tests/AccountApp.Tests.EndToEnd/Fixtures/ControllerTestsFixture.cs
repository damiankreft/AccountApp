using AccountApp.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace AccountApp.Tests.EndToEnd.Controllers
{
    public class ControllerTestsFixture : WebApplicationFactory<Startup>
    {
        public HttpClient _client;
        public readonly WebApplicationFactory<Startup> _factory;

        public ControllerTestsFixture()
        {
            _factory = new WebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
        }

        public HttpClient GetClient()
            => _client;
    }
}