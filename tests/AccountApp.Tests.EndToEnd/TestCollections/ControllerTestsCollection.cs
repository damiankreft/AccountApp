using AccountApp.Tests.EndToEnd.Controllers;
using Xunit;

namespace AccountApp.Tests.EndToEnd.Fixtures
{
    [CollectionDefinition("ControllerTests")]
    public class ControllerFixture : ICollectionFixture<ControllerTestsFixture> { }
}