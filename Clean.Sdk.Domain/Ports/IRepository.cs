using Clean.Sdk.Domain.Model;

namespace Clean.Sdk.Domain.Ports
{
	/// <summary>
	/// Defines the base repository interface for data access operations.
	/// </summary>
	public interface IRepository
	{
		/// <summary>
		/// Asynchronously deletes an entity by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the entity to delete.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains true if the entity was deleted; otherwise, false.</returns>
		Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken = default);

		/// <summary>
		/// Asynchronously persists all changes made in the context to the data store.
		/// </summary>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}

	/// <summary>
	/// Defines a generic repository interface for a specific domain model.
	/// </summary>
	/// <typeparam name="TModel">The type of the domain model.</typeparam>
	public interface IRepository<TModel> : IRepository
		where TModel : class, IDomainModel
	{
		/// <summary>
		/// Asynchronously saves a new domain model instance.
		/// </summary>
		/// <param name="model">The domain model to save.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the saved domain model.</returns>
		Task<TModel> SaveAsync(TModel model, CancellationToken cancellationToken = default);

		/// <summary>
		/// Asynchronously retrieves all instances of the domain model.
		/// </summary>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains an array of all domain models.</returns>
		Task<TModel[]> GetAllAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Asynchronously retrieves a domain model by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the domain model.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the domain model if found; otherwise, null.</returns>
		Task<TModel?> GetByIdAsync(object id, CancellationToken cancellationToken = default);

		/// <summary>
		/// Asynchronously updates an existing domain model instance.
		/// </summary>
		/// <param name="model">The domain model to update.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the updated domain model.</returns>
		Task<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken = default);

		/// <summary>
		/// Asynchronously deletes a specific domain model instance.
		/// </summary>
		/// <param name="model">The domain model to delete.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains true if the entity was deleted; otherwise, false.</returns>
		Task<bool> DeleteAsync(TModel model, CancellationToken cancellationToken = default);
	}
}
