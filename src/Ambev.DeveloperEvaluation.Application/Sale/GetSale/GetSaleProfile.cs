using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale
{
    /// <summary>
    /// AutoMapper profile for mapping Sale to GetSaleResult.
    /// </summary>
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<Domain.Entities.Sale, GetSaleResult>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
                .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.SaleItems));

            CreateMap<SaleItem, GetSaleItemDto>();
        }
    }
}
