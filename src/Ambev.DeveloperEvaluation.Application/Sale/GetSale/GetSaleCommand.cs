using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale
{
    /// <summary>
    /// Command to retrieve a sale by its ID.
    /// </summary>
    public class GetSaleCommand : IRequest<GetSaleResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }

        public GetSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
