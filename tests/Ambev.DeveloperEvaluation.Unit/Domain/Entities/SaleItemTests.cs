using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    /// <summary>
    /// Contains unit tests for the SaleItem entity.
    /// </summary>
    public class SaleItemTests
    {
        [Fact(DisplayName = "Total amount should be calculated correctly")]
        public void Given_ValidSaleItem_When_CalculateTotalAmount_Then_TotalAmountShouldBeCorrect()
        {
            // Arrange
            var saleItem = SaleTestData.GenerateValidSaleItems(1).First();

            // Act
            saleItem.CalculateTotalAmount();

            // Assert
            var expectedTotal = (saleItem.UnitPrice * saleItem.Quantity) - saleItem.Discount;
            Assert.Equal(expectedTotal, saleItem.TotalAmount);
        }

        [Fact(DisplayName = "Discount should be calculated correctly")]
        public void Given_ValidSaleItem_When_CalculateDiscount_Then_DiscountShouldBeCorrect()
        {
            // Arrange
            var saleItem = SaleTestData.GenerateValidSaleItems(1).First();

            // Act
            saleItem.CalculateDiscount();

            // Assert
            Assert.True(saleItem.Discount >= 0); // Ensure discount is non-negative
        }
    }
}
