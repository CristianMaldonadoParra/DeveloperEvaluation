using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sale.DeleteSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.DeleteSale
{
    /// <summary>
    /// AutoMapper profile for mapping DeleteSaleRequest to DeleteSaleCommand
    /// </summary>
    public class DeleteSaleProfile : Profile
    {
        public DeleteSaleProfile()
        {
            CreateMap<DeleteSaleRequest, DeleteSaleCommand>();
        }
    }
}

