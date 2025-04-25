using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale
{
    /// <summary>
    /// Validator for the GetSaleCommand.
    /// </summary>
    public class GetSaleValidator : AbstractValidator<GetSaleCommand>
    {
        public GetSaleValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale ID is required.")
                .NotEqual(Guid.Empty).WithMessage("Sale ID must be a valid GUID.");
        }
    }
}
