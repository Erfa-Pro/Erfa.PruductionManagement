using Erfa.ProductionManagement.Api.Test.Integration.Base;

namespace Erfa.ProductionManagement.Api.Test.Integration.Controllers
{
    public class MyTestFixture : IAsyncLifetime
    {

        public Task DisposeAsync()
        {
            DockerCompose.NewDockerComposeDown("Catalog");
            return Task.CompletedTask;

        }

        public Task InitializeAsync()
        {
            DockerCompose.NewDockerComposeUp("Catalog");
            return Task.CompletedTask;
        }

        [Fact]
        public void Test()
        {
            Assert.True(true);
        }
    }
}
