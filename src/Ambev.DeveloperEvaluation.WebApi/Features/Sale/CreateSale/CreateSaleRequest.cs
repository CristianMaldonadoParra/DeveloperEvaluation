using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale
{
    public class CreateSaleRequest
    {
        public string SaleNumber { get; set; } = string.Empty;

        public DateTime SaleDate { get; set; }

        public Guid CustomerId { get; set; }

        public string Branch { get; set; } = string.Empty;

        public List<CreateSaleItemDto> SaleItems { get; set; } = new();
    }
}
