using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sale.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale
{
    /// <summary>
    /// AutoMapper profile for mapping between GetSaleResult and GetSaleResponse.
    /// </summary>
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<GetSaleResult, GetSaleResponse>();
            CreateMap<GetSaleItemDto, GetSaleItemResponse>();

            CreateMap<Guid, GetSaleCommand>()
                .ConstructUsing(id => new GetSaleCommand(id));

        }
    }
}
