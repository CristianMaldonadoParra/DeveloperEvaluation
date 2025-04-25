using System.Net;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional
{
    /// <summary>
    /// Functional tests for the Sale API endpoints.
    /// </summary>
    public class SaleFunctionalTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public SaleFunctionalTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "Create Sale and Retrieve it Successfully")]
        public async Task Given_ValidSale_When_Created_Then_ShouldBeRetrievedSuccessfully()
        {
            
            var createSaleRequest = new CreateSaleRequest
            {
                SaleNumber = "SALE12345",
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(), // Replace with a valid CustomerId if needed
                Branch = "Filial 2",
                SaleItems = new List<CreateSaleItemDto>
                {
                    new CreateSaleItemDto { Product = "Produto A", Quantity = 2, UnitPrice = 50 },
                    new CreateSaleItemDto { Product = "Produto B", Quantity = 1, UnitPrice = 100 }
                }
            };

            
            var createResponse = await _client.PostAsJsonAsync("/api/sales", createSaleRequest);

            
            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

            var createdSale = await createResponse.Content.ReadFromJsonAsync<CreateSaleResponse>();
            Assert.NotNull(createdSale);
            

            
            var getResponse = await _client.GetAsync($"/api/sales/{createdSale.Id}");

            
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var retrievedSale = await getResponse.Content.ReadFromJsonAsync<GetSaleResponse>();
            Assert.NotNull(retrievedSale);
            Assert.Equal(createSaleRequest.SaleNumber, retrievedSale.SaleNumber);
            Assert.Equal(createSaleRequest.Branch, retrievedSale.Branch);
            Assert.Equal(createSaleRequest.SaleItems.Count, retrievedSale.SaleItems.Count);
        }

        [Fact(DisplayName = "Delete Sale Successfully")]
        public async Task Given_ExistingSale_When_Deleted_Then_ShouldNotBeRetrieved()
        {
            
            var createSaleRequest = new CreateSaleRequest
            {
                SaleNumber = "SALE67890",
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                Branch = "Filial Interior",
                SaleItems = new List<CreateSaleItemDto>
                {
                    new CreateSaleItemDto { Product = "Produto C", Quantity = 3, UnitPrice = 30 }
                }
            };

            
            var createResponse = await _client.PostAsJsonAsync("/api/sales", createSaleRequest);
            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

            var createdSale = await createResponse.Content.ReadFromJsonAsync<CreateSaleResponse>();
            Assert.NotNull(createdSale);

            
            var deleteResponse = await _client.DeleteAsync($"/api/sales/{createdSale.Id}");

            
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);

            
            var getResponse = await _client.GetAsync($"/api/sales/{createdSale.Id}");

            
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}
