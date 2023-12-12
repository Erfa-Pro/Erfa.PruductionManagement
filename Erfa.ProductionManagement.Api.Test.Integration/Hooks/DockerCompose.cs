using BoDi;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Microsoft.Extensions.Configuration;
using System.Net;


namespace Erfa.ProductionManagement.Api.Test.Integration.Base
{
    public class DockerCompose
    {
        public static Dictionary<string, ICompositeService> _services = new Dictionary<string, ICompositeService>();

        public static void NewDockerComposeUp(string controllerName)
        {
            var config = LoadConfiguration();

            var dockerComposeFileName = config[$"{controllerName}:DockerComposeFile"];
            var dockerComposePath = GetDockerComposeLocation(dockerComposeFileName);

            var confirmationUrl = config[$"{controllerName}:ProductionManagement.Api:BaseAddress"];
            var compositeService = new Builder()
                .UseContainer()
                .UseCompose()
                .RemoveAllImages()
                .FromFile(dockerComposePath)
                .RemoveOrphans()
                .WaitForHttp("webapi", $"{confirmationUrl}/",
                    continuation: (response, _) => response.Code != HttpStatusCode.OK ? 2000 : 0)
                .Build()
                .Start();
            _services.Add(controllerName, compositeService);
        }

        public static void NewDockerComposeDown(string controllerName)
        {
            var compositeService = _services.FirstOrDefault(s => s.Key == controllerName);
            compositeService.Value.Stop();
            compositeService.Value.Remove(true);
            compositeService.Value.Dispose();
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
