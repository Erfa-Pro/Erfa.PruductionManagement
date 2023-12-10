using Erfa.ProductionManagement.Service.Test.Acceptance.Models.Catalog;
using Erfa.ProductionManagement.Service.Test.Acceptance.Models.Catalog.Requests;
using System;
using System.Net;
using System.Net.Http.Json;
using TechTalk.SpecFlow;

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

        [Given(@"valid CreateProductRequest with ProductNumber ""([^""]*)"", Description ""([^""]*)"", ProductionTimeSec (.*), MaterialProductName ""([^""]*)""")]
        public void GivenValidCreateProductRequestWithProductNumberDescriptionProductionTimeSecMaterialProductName(
                            string productNumber,
                            string description,
                            double productionTimeSec,
                            string materialProductName)
        {
            CreateProductRequest request = new CreateProductRequest()
            {
                ProductNumber = productNumber,
                Description = description,
                ProductionTimeSec = productionTimeSec,
                MaterialProductName = materialProductName
            };


            _scenarioContext.Add("ValidRequest", request);
        }

        [When(@"creating a new product")]
        public async Task WhenCreatingANewProduct()
        {
            var response = await _httpClient.PostAsJsonAsync("/api/v1/Catalog/Create", _scenarioContext.Get<CreateProductRequest>("ValidRequest"));
            var isSuccess = response.IsSuccessStatusCode;
            var responseContentString = await response.Content.ReadAsStringAsync();
            _scenarioContext.Add("ValidRequestResponseIsSuccess", isSuccess);
            _scenarioContext.Add("ValidRequestResponseContentString", responseContentString);
            
        }

        [Then(@"the response status is Success")]
        public void ThenTheResponseStatusIsSuccess()
        {
            var isSuccess = _scenarioContext.Get<Boolean>("ValidRequestResponseIsSuccess");

            Assert.True(isSuccess);
        }

        [Then(@"the product is created with ProductNumber ""([^""]*)""")]
        public void ThenTheProductIsCreatedWithProductNumber(string productNumber)
        {
            var responseProduct = _scenarioContext.Get<string>("ValidRequestResponseContentString");
            Assert.True(responseProduct.Equals(productNumber));
        }
    }
}
