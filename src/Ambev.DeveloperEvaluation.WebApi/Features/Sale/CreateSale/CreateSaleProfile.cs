using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale
{
    /// <summary>
    /// AutoMapper profile for mapping CreateSaleRequest to CreateSaleCommand.
    /// </summary>
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleRequest, CreateSaleCommand>();
            CreateMap<CreateSaleItemDto, Application.Sale.CreateSale.CreateSaleItemDto>();
            CreateMap<CreateSaleResult, CreateSaleResponse>();
        }
    }
}
