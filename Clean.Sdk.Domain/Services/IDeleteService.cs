using Clean.Sdk.Domain.Model;

namespace Clean.Sdk.Domain.Services
{
	/// <summary>
	/// Defines deletion operations for a domain model.
	/// </summary>
	/// <typeparam name="TModel">The type of the domain model.</typeparam>
	public interface IDeleteService<TModel>
		where TModel : class, IDomainModel
	{
		/// <summary>
		/// Asynchronously deletes a specific entity instance.
		/// </summary>
		/// <param name="entity">The entity to delete.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains true if the entity was deleted; otherwise, false.</returns>
		Task<bool> DeleteAsync(TModel entity, CancellationToken cancellationToken);

		/// <summary>
		/// Asynchronously deletes an entity by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the entity to delete.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains true if the entity was deleted; otherwise, false.</returns>
		Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken);
	}
}
