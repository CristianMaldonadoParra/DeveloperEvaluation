namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale
{
    /// <summary>
    /// Represents the response for retrieving a sale.
    /// </summary>
    public class GetSaleResponse
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public string Branch { get; set; } = string.Empty;
        public List<GetSaleItemResponse> SaleItems { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }

    public class GetSaleItemResponse
    {
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
