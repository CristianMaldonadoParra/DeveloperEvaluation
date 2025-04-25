using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            // Validate SaleNumber
            RuleFor(x => x.SaleNumber)
                .NotEmpty().WithMessage("Sale number is required.")
                .MaximumLength(50).WithMessage("Sale number must not exceed 50 characters.");

            // Validate SaleDate
            RuleFor(x => x.SaleDate)
                .NotEmpty().WithMessage("Sale date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

            // Validate CustomerId
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required.")
                .NotEqual(Guid.Empty).WithMessage("Customer ID must be a valid GUID.");

            // Validate Branch
            RuleFor(x => x.Branch)
                .NotEmpty().WithMessage("Branch is required.")
                .MaximumLength(100).WithMessage("Branch must not exceed 100 characters.");

            // Validate SaleItems
            RuleFor(x => x.SaleItems)
                .NotEmpty().WithMessage("At least one sale item is required.")
                .Must(items => items.All(item => item != null)).WithMessage("Sale items must not contain null values.");

        }
    }
}
