using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(sale => sale.Customer)
                .NotNull()
                .WithMessage("Customer information is required.");
            RuleFor(sale => sale.Branch)
                .NotEmpty()
                .WithMessage("Branch information is required.")
                .MaximumLength(100)
                .WithMessage("Branch name cannot exceed 100 characters.");
            RuleFor(sale => sale.SaleItems)
                .NotEmpty()
                .WithMessage("At least one sale item is required.")
                .Must(items => items.All(item => item.Quantity > 0))
                .WithMessage("All sale items must have a quantity greater than zero.");
            RuleFor(sale => sale.TotalAmount)
                .GreaterThan(0)
                .WithMessage("Total amount must be greater than zero.");
        }
    }
}
