namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale
{
    /// <summary>
    /// Represents a request to retrieve a sale by its ID.
    /// </summary>
    public class GetSaleRequest
    {
        public Guid Id { get; set; }
    }
}
