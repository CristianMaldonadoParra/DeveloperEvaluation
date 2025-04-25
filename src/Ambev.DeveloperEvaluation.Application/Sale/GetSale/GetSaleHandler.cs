using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale
{
    /// <summary>
    /// Handles the retrieval of a sale by its ID.
    /// </summary>
    public class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the GetSaleHandler class.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the retrieval of a sale by its ID.
        /// </summary>
        /// <param name="command">The command containing the sale ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the sale retrieval.</returns>
        public async Task<GetSaleResult> Handle(GetSaleCommand command, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {command.Id} not found.");

            return _mapper.Map<GetSaleResult>(sale);
        }
    }
}
