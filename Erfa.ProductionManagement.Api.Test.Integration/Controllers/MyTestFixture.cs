using Erfa.ProductionManagement.Api.Test.Integration.Base;

namespace Erfa.ProductionManagement.Api.Test.Integration.Controllers
{
    public class MyTestFixture : IAsyncLifetime
    {
        private readonly DockerCompose _dockerCompose;

        public MyTestFixture()
        {
            _dockerCompose = new DockerCompose();
        }

        public Task DisposeAsync()
        {
            _dockerCompose.DockerComposeDown();
            return Task.CompletedTask;

        }

        public Task InitializeAsync()
        {
            _dockerCompose.DockerComposeUp("Catalog");
            return Task.CompletedTask;
        }

        [Fact]
        public void Test ()
        {
            Assert.True(true);
        }
    }
}
