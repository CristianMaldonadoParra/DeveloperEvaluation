﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sale.DeleteSale
{
    /// <summary>
    /// Command for deleting a sale
    /// </summary>
    public record DeleteSaleCommand : IRequest<DeleteSaleResponse>
    {
        /// <summary>
        /// The unique identifier of the sale to delete
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of DeleteSaleCommand
        /// </summary>
        /// <param name="id">The ID of the sale to delete</param>
        public DeleteSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
