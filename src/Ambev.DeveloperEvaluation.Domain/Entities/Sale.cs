using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {/// <summary>
     /// Gets or sets the sale number.
     /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the sale was made.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the customer information associated with the sale.
        /// </summary>
        public User Customer { get; set; }

        /// <summary>
        /// Gets or sets the branch where the sale was made.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of sale items in the sale.
        /// </summary>
        public List<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

        /// <summary>
        /// Gets or sets the total sale amount (after discounts).
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the status of the sale (e.g., Cancelled or Not Cancelled).
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// Initializes a new instance of the Sale class and calculates the total amount.
        /// </summary>
        public Sale()
        {
            SaleDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Calculates the total sale amount based on the items and their quantities.
        /// Applies business rules for discounts.
        /// </summary>
        public void CalculateTotalAmount()
        {
            TotalAmount = SaleItems.Sum(item => item.TotalAmount);
        }

        /// <summary>
        /// Cancels the sale by marking it as cancelled.
        /// </summary>
        public void CancelSale()
        {
            IsCancelled = true;
            foreach (var item in SaleItems)
            {
                item.CancelItem();
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
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
