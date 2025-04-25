namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale
{
    /// <summary>
    /// Result of the GetSaleCommand.
    /// </summary>
    public class GetSaleResult
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public string Branch { get; set; } = string.Empty;
        public List<GetSaleItemDto> SaleItems { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }

    public class GetSaleItemDto
    {
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
