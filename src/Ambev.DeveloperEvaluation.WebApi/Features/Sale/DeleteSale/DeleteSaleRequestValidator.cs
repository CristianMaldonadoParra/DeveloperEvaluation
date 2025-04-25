using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.DeleteSale
{
    /// <summary>
    /// Validator for DeleteSaleRequest
    /// </summary>
    public class DeleteSaleRequestValidator : AbstractValidator<DeleteSaleRequest>
    {
        /// <summary>
        /// Initializes validation rules for DeleteSaleRequest
        /// </summary>
        public DeleteSaleRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sale ID is required.")
                .NotEqual(Guid.Empty)
                .WithMessage("Sale ID must be a valid GUID.");
        }
    }
}

