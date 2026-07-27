using Clean.Sdk.Domain.Model;

namespace Clean.Sdk.Domain.Services
{
	/// <summary>
	/// Defines update operations for a domain model.
	/// </summary>
	/// <typeparam name="TModel">The type of the domain model.</typeparam>
	public interface IUpdateService<TModel>
		where TModel : class, IDomainModel
	{
		/// <summary>
		/// Asynchronously updates an existing entity instance.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
		Task<TModel> UpdateAsync(TModel entity, CancellationToken cancellationToken);
	}
}
