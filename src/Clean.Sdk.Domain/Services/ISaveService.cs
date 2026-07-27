using Clean.Sdk.Domain.Model;

namespace Clean.Sdk.Domain.Services
{
	/// <summary>
	/// Defines save operations for a domain model.
	/// </summary>
	/// <typeparam name="TModel">The type of the domain model.</typeparam>
	public interface ISaveService<TModel>
		where TModel : class, IDomainModel
	{
		/// <summary>
		/// Asynchronously saves a new entity instance.
		/// </summary>
		/// <param name="entity">The entity to save.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the saved entity.</returns>
		Task<TModel> SaveAsync(TModel entity, CancellationToken cancellationToken);
	}
}
