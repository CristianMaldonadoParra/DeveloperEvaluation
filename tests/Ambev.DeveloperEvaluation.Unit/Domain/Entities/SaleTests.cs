using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    /// <summary>
    /// Contains unit tests for the Sale entity.
    /// </summary>
    public class SaleTests
    {
        [Fact(DisplayName = "Total amount should be calculated correctly")]
        public void Given_ValidSale_When_CalculateTotalAmount_Then_TotalAmountShouldBeCorrect()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            sale.CalculateTotalAmount();

            // Assert
            var expectedTotal = sale.SaleItems.Sum(item => item.TotalAmount);
            Assert.Equal(expectedTotal, sale.TotalAmount);
        }

        [Fact(DisplayName = "Sale should be marked as cancelled")]
        public void Given_ValidSale_When_CancelSale_Then_IsCancelledShouldBeTrue()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            sale.CancelSale();

            // Assert
            Assert.True(sale.IsCancelled);
            Assert.All(sale.SaleItems, item => Assert.Equal(0, item.TotalAmount));
        }

        [Fact(DisplayName = "Validation should pass for valid sale data")]
        public void Given_ValidSale_When_Validated_Then_ShouldReturnValid()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            var result = sale.Validate();

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact(DisplayName = "Validation should fail for invalid sale data")]
        public void Given_InvalidSale_When_Validated_Then_ShouldReturnInvalid()
        {
            // Arrange
            var sale = SaleTestData.GenerateInvalidSale();

            // Act
            var result = sale.Validate();

            // Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact(DisplayName = "Discount should be 10% for purchases above 4 identical items")]
        public void Given_SaleItemWithQuantityAbove4_When_CalculateDiscount_Then_DiscountShouldBe10Percent()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var saleItem = sale.SaleItems.First();
            saleItem.Quantity = 5;

            // Act
            saleItem.CalculateDiscount();

            // Assert
            var expectedDiscount = saleItem.UnitPrice * saleItem.Quantity * 0.10m;
            Assert.Equal(expectedDiscount, saleItem.Discount);
        }

        [Fact(DisplayName = "Discount should be 20% for purchases between 10 and 20 identical items")]
        public void Given_SaleItemWithQuantityBetween10And20_When_CalculateDiscount_Then_DiscountShouldBe20Percent()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var saleItem = sale.SaleItems.First();
            saleItem.Quantity = 15;

            // Act
            saleItem.CalculateDiscount();

            // Assert
            var expectedDiscount = saleItem.UnitPrice * saleItem.Quantity * 0.20m;
            Assert.Equal(expectedDiscount, saleItem.Discount);
        }

        [Fact(DisplayName = "It should not be possible to sell above 20 identical items")]
        public void Given_SaleItemWithQuantityAbove20_When_Validated_Then_ShouldReturnInvalid()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var saleItem = sale.SaleItems.First();
            saleItem.Quantity = 21;

            // Act
            var result = sale.Validate();

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.Error.Contains("cannot sell above 20 identical items"));
        }

        [Fact(DisplayName = "No discount should be applied for purchases below 4 identical items")]
        public void Given_SaleItemWithQuantityBelow4_When_CalculateDiscount_Then_DiscountShouldBeZero()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            var saleItem = sale.SaleItems.First();
            saleItem.Quantity = 3;

            // Act
            saleItem.CalculateDiscount();

            // Assert
            Assert.Equal(0, saleItem.Discount);
        }
    }
}
