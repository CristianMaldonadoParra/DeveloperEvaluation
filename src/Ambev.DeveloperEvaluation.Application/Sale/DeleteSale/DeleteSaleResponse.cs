using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sale.DeleteSale
{
    /// <summary>
    /// Response for DeleteSaleCommand
    /// </summary>
    public class DeleteSaleResponse
    {
        /// <summary>
        /// Indicates whether the sale was successfully deleted
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Message providing additional information about the operation
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
