using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale
{
    /// <summary>
    /// Validator for the GetSaleRequest.
    /// </summary>
    public class GetSaleRequestValidator : AbstractValidator<GetSaleRequest>
    {
        public GetSaleRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale ID is required.")
                .NotEqual(Guid.Empty).WithMessage("Sale ID must be a valid GUID.");
        }
    }
}
