using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.DeleteSale
{
    /// <summary>
    /// Handles the deletion of a sale
    /// </summary>
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
    {
        private readonly ISaleRepository _saleRepository;

        /// <summary>
        /// Initializes a new instance of DeleteSaleHandler
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        public DeleteSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        /// <summary>
        /// Handles the deletion of a sale
        /// </summary>
        /// <param name="command">The command containing the sale ID</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The result of the deletion operation</returns>
        public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
        {
            var success = await _saleRepository.DeleteAsync(command.Id, cancellationToken);

            return new DeleteSaleResponse
            {
                Success = success,
                Message = success ? "Sale deleted successfully." : "Sale not found."
            };
        }
    }
}

