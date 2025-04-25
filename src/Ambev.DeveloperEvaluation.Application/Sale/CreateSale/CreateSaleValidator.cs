using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale
{
    /// <summary>
    /// Validator for CreateSaleCommand that defines validation rules for sale creation command.
    /// </summary>
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - SaleNumber: Required, must be between 1 and 50 characters
        /// - SaleDate: Must be a valid date and not in the future
        /// - CustomerId: Required and must be a valid GUID
        /// - Branch: Required, must be between 1 and 100 characters
        /// - SaleItems: Must not be empty, and each item must be valid (using SaleItemValidator)
        /// </remarks>
        public CreateSaleValidator()
        {
            RuleFor(sale => sale.SaleNumber)
                .NotEmpty()
                .Length(1, 50)
                .WithMessage("Sale number is required and must be between 1 and 50 characters.");

            RuleFor(sale => sale.SaleDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Sale date must be a valid date and cannot be in the future.");

            RuleFor(sale => sale.CustomerId)
                .NotEmpty()
                .WithMessage("Customer ID is required.")
                .Must(id => id != Guid.Empty)
                .WithMessage("Customer ID must be a valid GUID.");

            RuleFor(sale => sale.Branch)
                .NotEmpty()
                .Length(1, 100)
                .WithMessage("Branch is required and must be between 1 and 100 characters.");

            RuleFor(sale => sale.SaleItems)
                .NotEmpty()
                .WithMessage("At least one sale item is required.")
                .ForEach(item => item.SetValidator(new CreateSaleItemValidator()));
        }
    }

    /// <summary>
    /// Validator for CreateSaleItemDto that defines validation rules for sale items.
    /// </summary>
    public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemDto>
    {
        /// <summary>
        /// Initializes a new instance of the CreateSaleItemValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - Product: Required, must be between 1 and 100 characters
        /// - Quantity: Must be greater than 0
        /// - UnitPrice: Must be greater than or equal to 0
        /// </remarks>
        public CreateSaleItemValidator()
        {
            RuleFor(item => item.Product)
                .NotEmpty()
                .Length(1, 100)
                .WithMessage("Product name is required and must be between 1 and 100 characters.");

            RuleFor(item => item.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");

            RuleFor(item => item.UnitPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Unit price must be greater than or equal to 0.");
        }
    }
}
