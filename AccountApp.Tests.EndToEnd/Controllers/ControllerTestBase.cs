using System.Net.Http;
using AccountApp.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AccountApp.Tests.EndToEnd.Controllers
{
    public class ControllerTestBase : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly WebApplicationFactory<Startup> _factory;
        protected HttpClient _client;

        public ControllerTestBase(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }
    }
}