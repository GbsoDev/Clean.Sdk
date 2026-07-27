using Clean.Sdk.Domain.Model;

namespace Clean.Sdk.Domain.Services
{
	/// <summary>
	/// Defines CRUD operations for a domain model.
	/// </summary>
	/// <typeparam name="TModel">The type of the domain model.</typeparam>
	internal interface ICrudService<TModel>
		where TModel : class, IDomainModel
	{
		/// <summary>
		/// Asynchronously deletes an entity by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the entity.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task DeleteByIdAsync(object id, CancellationToken cancellationToken = default);

		/// <summary>
		/// Asynchronously retrieves an entity by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the entity.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task representing the asynchronous operation. The task result contains the entity if found; otherwise, null.</returns>
		Task<TModel?> GetByIdAsync(object id, CancellationToken cancellationToken = default);

		/// <summary>
		/// Asynchronously retrieves all entities.
		/// </summary>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task representing the asynchronous operation. The task result contains an array of entities.</returns>
		Task<TModel[]> LisAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Asynchronously saves a new entity.
		/// </summary>
		/// <param name="entity">The entity to save.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task representing the asynchronous operation. The task result contains the saved entity.</returns>
		Task<TModel> SaveAsync(TModel entity, CancellationToken cancellationToken = default);

		/// <summary>
		/// Asynchronously updates an existing entity.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task representing the asynchronous operation. The task result contains the updated entity.</returns>
		Task<TModel> UpdateAsync(TModel entity, CancellationToken cancellationToken = default);
	}
}
