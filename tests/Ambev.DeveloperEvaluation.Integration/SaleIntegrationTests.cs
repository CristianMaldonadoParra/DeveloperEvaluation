using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration
{
    /// <summary>
    /// Integration tests for Sale and SaleItem entities using an in-memory database.
    /// </summary>
    public class SaleIntegrationTests
    {
        private DbContextOptions<DefaultContext> CreateInMemoryDatabaseOptions()
        {
            return new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact(DisplayName = "Sale and SaleItems should be persisted and retrieved correctly")]
        public async Task Given_ValidSale_When_SavedToDatabase_Then_ShouldBeRetrievedCorrectly()
        {
            // Arrange
            var options = CreateInMemoryDatabaseOptions();
            var sale = new Sale
            {
                SaleNumber = "SALE12345",
                SaleDate = DateTime.UtcNow,
                Branch = "Filial SP 1",
                SaleItems = new[]
                {
                    new SaleItem { Product = "Produto A", Quantity = 2, UnitPrice = 50 },
                    new SaleItem { Product = "Produto B", Quantity = 1, UnitPrice = 100 }
                }.ToList()
            };
            sale.CalculateTotalAmount();

            // Act
            using (var context = new DefaultContext(options))
            {
                context.Sales.Add(sale);
                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new DefaultContext(options))
            {
                var retrievedSale = await context.Sales
                    .Include(s => s.SaleItems)
                    .FirstOrDefaultAsync(s => s.SaleNumber == "SALE12345");

                Assert.NotNull(retrievedSale);
                Assert.Equal(sale.SaleNumber, retrievedSale.SaleNumber);
                Assert.Equal(sale.SaleDate, retrievedSale.SaleDate);
                Assert.Equal(sale.Branch, retrievedSale.Branch);
                Assert.Equal(sale.TotalAmount, retrievedSale.TotalAmount);
                Assert.Equal(2, retrievedSale.SaleItems.Count);

                var retrievedItemA = retrievedSale.SaleItems.FirstOrDefault(i => i.Product == "Produto A");
                Assert.NotNull(retrievedItemA);
                Assert.Equal(2, retrievedItemA.Quantity);
                Assert.Equal(50, retrievedItemA.UnitPrice);

                var retrievedItemB = retrievedSale.SaleItems.FirstOrDefault(i => i.Product == "Produto B");
                Assert.NotNull(retrievedItemB);
                Assert.Equal(1, retrievedItemB.Quantity);
                Assert.Equal(100, retrievedItemB.UnitPrice);
            }
        }

        [Fact(DisplayName = "Sale should be deleted correctly")]
        public async Task Given_ExistingSale_When_DeletedFromDatabase_Then_ShouldNotBeRetrieved()
        {
            // Arrange
            var options = CreateInMemoryDatabaseOptions();
            var sale = new Sale
            {
                SaleNumber = "SALE12345",
                SaleDate = DateTime.UtcNow,
                Branch = "Filial SP 1",
                SaleItems = new[]
                {
                    new SaleItem { Product = "Produto A", Quantity = 2, UnitPrice = 50 }
                }.ToList()
            };

            using (var context = new DefaultContext(options))
            {
                context.Sales.Add(sale);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new DefaultContext(options))
            {
                var saleToDelete = await context.Sales.FirstOrDefaultAsync(s => s.SaleNumber == "SALE12345");
                Assert.NotNull(saleToDelete);

                context.Sales.Remove(saleToDelete);
                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new DefaultContext(options))
            {
                var deletedSale = await context.Sales.FirstOrDefaultAsync(s => s.SaleNumber == "SALE12345");
                Assert.Null(deletedSale);
            }
        }
    }
}
