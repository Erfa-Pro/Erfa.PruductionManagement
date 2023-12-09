using Erfa.ProductionManagement.Service.Test.Acceptance.Models.Catalog;
using Erfa.ProductionManagement.Service.Test.Acceptance.Models.Catalog.Requests;
using System.Net.Http.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Erfa.ProductionManagement.Test.Acceptance.StepDefinitions
{
    [Binding]
    public class CreateProductStepDefinitions
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public CreateProductStepDefinitions(HttpClient httpClient, ScenarioContext scenarioContext)
        {
            _httpClient = httpClient;
            _scenarioContext = scenarioContext;
        }

        [Given(@"valid CreateProductRequest:")]
        public async void GivenValidCreateProductRequest(Table table)
        {
            var createdProductRequests = table.CreateSet<CreateProductRequest>();
            var createdProducts = new List<ProductModelView>();
            foreach (var createProductRequest in createdProductRequests)
            {
                var response = await _httpClient.PostAsJsonAsync("products", createProductRequest);
                var responseProduct = await response.Content.ReadFromJsonAsync<ProductModelView>();
                createdProducts.Add(responseProduct);
            }
            _scenarioContext.Add("CreatedProducts", createdProducts);
            var x = 3;
        }

        [When(@"I create a product")]
        public void WhenICreateAProduct()
        {
            ScenarioContext.StepIsPending();
        }

        [Then(@"the product is created")]
        public void ThenTheProductIsCreated()
        {
            ScenarioContext.StepIsPending();
        }

        [Given(@"invalid CreateProductRequest")]
        public void GivenInvalidCreateProductRequest()
        {
            ScenarioContext.StepIsPending();
        }

        [Then(@"the product is not created")]
        public void ThenTheProductIsNotCreated()
        {
            ScenarioContext.StepIsPending();
        }
    }
}
