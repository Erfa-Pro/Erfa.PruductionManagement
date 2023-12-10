using BoDi;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using TechTalk.SpecFlow;

namespace Erfa.ProductionManagement.Test.Acceptance.Hooks
{
    [Binding]
    public class DockerControllerHook
    {
        public static ICompositeService _compositeService;
        private IObjectContainer _objectContainer;
        public DockerControllerHook(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void DockerComposeUp()
        {
            var config = LoadConfiguration();

            var dockerComposeFileName = config["DockerComposeFile"];
            var dockerComposePath = GetDockerComposeLocation(dockerComposeFileName);

            var confirmationUrl = config["ProductionManagement.Api:BaseAddress"];
            _compositeService = new Builder()
                .UseContainer()
                .UseCompose()
                .RemoveAllImages()
                .FromFile(dockerComposePath)
                .RemoveOrphans()
                .ForceRecreate()
                .WaitForHttp("webapi", $"{confirmationUrl}/",
                    continuation: (response, _) => response.Code != HttpStatusCode.OK ? 2000 : 0)
                .Build()
                .Start();
        }

        [AfterTestRun]
        public static void DockerComposeDown()
        {

            _compositeService.Stop();

            _compositeService.Remove(true);
            _compositeService.Dispose();

        }

        [BeforeScenario]
        public void AddHttpClient()
        {
            var config = LoadConfiguration();
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(config["ProductionManagement.Api:BaseAddress"])

            };

            httpClient.DefaultRequestHeaders.Add("UserName", "TesUser");
            _objectContainer.RegisterInstanceAs<HttpClient>(httpClient);
        }

        private static IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
        private static string GetDockerComposeLocation(string dockerComposeFileName)
        {
            var directory = Directory.GetCurrentDirectory();
            while (!Directory.EnumerateFiles(directory, "*.yml").Any(s => s.EndsWith(dockerComposeFileName)))
            {
                directory = directory.Substring(0, directory.LastIndexOf(Path.DirectorySeparatorChar));
            }
            return Path.Combine(directory, dockerComposeFileName);
        }
    }
}
