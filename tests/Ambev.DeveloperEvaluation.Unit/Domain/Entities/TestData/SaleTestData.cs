using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    /// <summary>
    /// Provides test data for Sale and SaleItem entities.
    /// </summary>
    public static class SaleTestData
    {
        private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
            .RuleFor(s => s.SaleNumber, f => f.Random.AlphaNumeric(10))
            .RuleFor(s => s.SaleDate, f => f.Date.Past())
            .RuleFor(s => s.Branch, f => f.Company.CompanyName())
            .RuleFor(s => s.SaleItems, f => GenerateValidSaleItems(3).ToList())
            .RuleFor(s => s.TotalAmount, 0) // Will be calculated
            .RuleFor(s => s.IsCancelled, false);

        private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
            .RuleFor(si => si.Product, f => f.Commerce.ProductName())
            .RuleFor(si => si.Quantity, f => f.Random.Int(1, 100))
            .RuleFor(si => si.UnitPrice, f => f.Random.Decimal(1, 100))
            .RuleFor(si => si.Discount, 0) // Will be calculated
            .RuleFor(si => si.TotalAmount, 0); // Will be calculated

        /// <summary>
        /// Generates a valid Sale entity.
        /// </summary>
        public static Sale GenerateValidSale()
        {
            var sale = SaleFaker.Generate();
            sale.CalculateTotalAmount();
            return sale;
        }

        /// <summary>
        /// Generates a list of valid SaleItem entities.
        /// </summary>
        public static IEnumerable<SaleItem> GenerateValidSaleItems(int count)
        {
            var items = SaleItemFaker.Generate(count);
            foreach (var item in items)
            {
                item.CalculateDiscount();
                item.CalculateTotalAmount();
            }
            return items;
        }

        /// <summary>
        /// Generates an invalid Sale entity.
        /// </summary>
        public static Sale GenerateInvalidSale()
        {
            return new Sale
            {
                SaleNumber = "", // Invalid: empty
                SaleDate = DateTime.UtcNow.AddDays(1), // Invalid: future date
                Branch = "", // Invalid: empty
                SaleItems = new List<SaleItem>(), // Invalid: no items
                TotalAmount = 0,
                IsCancelled = false
            };
        }
    }
}
