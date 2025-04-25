using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale
{
    /// <summary>
    /// AutoMapper profile for mapping between CreateSaleCommand, Sale, and CreateSaleResult.
    /// </summary>
    public class CreateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleProfile"/> class.
        /// </summary>
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Domain.Entities.Sale>();
            CreateMap<CreateSaleItemDto, SaleItem>();
            CreateMap<Domain.Entities.Sale, CreateSaleResult>();
        }
    }
}
