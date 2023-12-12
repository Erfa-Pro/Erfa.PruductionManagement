using BoDi;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Microsoft.Extensions.Configuration;
using System.Net;


namespace Erfa.ProductionManagement.Api.Test.Integration.Base
{
    public class DockerCompose
    {
        public ICompositeService _compositeService;
        //private IObjectContainer _objectContainer;
        //public DockerCompose(IObjectContainer objectContainer)
        //{
        //    _objectContainer = objectContainer;
        //}

        public void DockerComposeUp(string controllerName)
        {
            var config = LoadConfiguration();

            var dockerComposeFileName = config[$"{controllerName}:DockerComposeFile"];
            var dockerComposePath = GetDockerComposeLocation(dockerComposeFileName);

            var confirmationUrl = config[$"{controllerName}:ProductionManagement.Api:BaseAddress"];
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

        public void DockerComposeDown()
        {

            _compositeService.Stop();

            _compositeService.Remove(true);
            _compositeService.Dispose();

        }

        //public void AddHttpClient(string controllerName)
        //{
        //    var config = LoadConfiguration();
        //    var httpClient = new HttpClient()
        //    {
        //        BaseAddress = new Uri(config[$"{controllerName}:ProductionManagement.Api:BaseAddress"])

        //    };

        //    httpClient.DefaultRequestHeaders.Add("UserName", "TesUser");
        //    _objectContainer.RegisterInstanceAs<HttpClient>(httpClient);
        //}

        private IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
        private string GetDockerComposeLocation(string dockerComposeFileName)
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
