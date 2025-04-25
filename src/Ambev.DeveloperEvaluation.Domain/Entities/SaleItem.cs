using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        /// <summary>
        /// Gets or sets the product associated with the sale item.
        /// </summary>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product being sold.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount applied to the sale item based on quantity.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets the total amount for the sale item (unit price * quantity - discount).
        /// </summary>
        public decimal TotalAmount { get; private set; }

        /// <summary>
        /// Cancels the sale item by applying a 100% discount.
        /// </summary>
        public void CancelItem()
        {
            Discount = UnitPrice * Quantity; // Full discount on cancellation
        }

        public void CalculateTotalAmount()
        {
            TotalAmount = (Quantity * UnitPrice) - Discount;
        }

        /// <summary>
        /// Calculates the discount for the sale item based on quantity.
        /// </summary>
        public void CalculateDiscount()
        {
            if (Quantity >= 4 && Quantity < 10)
            {
                Discount = (UnitPrice * Quantity) * 0.10m; // 10% discount for 4+ items
            }
            else if (Quantity >= 10 && Quantity <= 20)
            {
                Discount = (UnitPrice * Quantity) * 0.20m; // 20% discount for 10-20 items
            }
            else
            {
                Discount = 0; // No discount if less than 4 items
            }
        }

        /// <summary>
        /// Performs validation of the sale entity using the SaleValidator rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - IsValid: Indicates whether all validation rules passed
        /// - Errors: Collection of validation errors if any rules failed
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleItemValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
