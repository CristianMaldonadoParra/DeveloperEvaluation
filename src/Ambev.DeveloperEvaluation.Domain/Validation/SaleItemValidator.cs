using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public  class SaleItemValidator:AbstractValidator<SaleItem>
    {
        public SaleItemValidator() 
        {
            RuleFor(saleItem => saleItem.Product)
                .NotEmpty()
                .WithMessage("Product name is required.")
                .MaximumLength(100)
                .WithMessage("Product name cannot exceed 100 characters.");
            RuleFor(saleItem => saleItem.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
            RuleFor(saleItem => saleItem.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit price must be greater than zero.");
            RuleFor(saleItem => saleItem.Discount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Discount cannot be negative.");   
            RuleFor(saleItem => saleItem.TotalAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total amount cannot be negative.");
            RuleFor(saleItem => saleItem.TotalAmount)
                .Equal(saleItem => (saleItem.UnitPrice * saleItem.Quantity) - saleItem.Discount)
                .WithMessage("Total amount must be equal to (Unit Price * Quantity) - Discount.");

        }
    }
}
