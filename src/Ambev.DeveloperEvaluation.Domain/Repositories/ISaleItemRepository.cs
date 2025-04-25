using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleItemRepository
    {
        /// <summary>
        /// Creates a new sale item in the repository.
        /// </summary>
        /// <param name="item">The sale item to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created sale item.</returns>
        Task<SaleItem> CreateAsync(SaleItem item, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a sale item by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sale item.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The sale item if found, null otherwise.</returns>
        Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a sale item from the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the sale item to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the sale item was deleted, false if not found.</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Creates a new sale item in the repository.
        /// </summary>
        /// <param name="item">The sale item to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created sale item.</returns>
        Task<SaleItem> UpdateAsync(SaleItem item, CancellationToken cancellationToken = default);
    }
}
