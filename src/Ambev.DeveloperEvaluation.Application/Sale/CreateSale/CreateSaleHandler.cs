
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale
{
    /// <summary>
    /// Handles the creation of a new sale.
    /// </summary>
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The sale repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public CreateSaleHandler(ISaleRepository saleRepository, IUserRepository userRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the creation of a new sale.
        /// </summary>
        /// <param name="command">The command containing sale details.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the sale creation.</returns>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.CustomerId, cancellationToken);
            if (user == null)
                throw new InvalidOperationException($"Customer with ID {command.CustomerId} not found.");

            var sale = new Domain.Entities.Sale
            {
                SaleNumber = command.SaleNumber,
                SaleDate = command.SaleDate,
                Customer = user,
                Branch = command.Branch,
                SaleItems = command.SaleItems.Select(item => new SaleItem
                {
                    Product = item.Product,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };

            foreach (var item in sale.SaleItems)
            {
                item.CalculateDiscount();
                item.CalculateTotalAmount();
            }

            sale.CalculateTotalAmount();

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);
            
            var result = _mapper.Map<CreateSaleResult>(createdSale);
            return result;

        }
    }
}
