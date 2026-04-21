using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Ports;

namespace Clean.Sdk.Domain.Services
{
	/// <summary>
	/// Base class for services that handle the deletion of domain models.
	/// </summary>
	/// <typeparam name="TModel">The type of the domain model.</typeparam>
	/// <typeparam name="TRepository">The type of the repository.</typeparam>
	public abstract class DeleteService<TModel, TRepository> : ActionService<TModel, TRepository>, IDeleteService<TModel>
		where TModel : class, IDomainModel
		where TRepository : IRepository<TModel>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DeleteService{TModel, TRepository}"/> class.
		/// </summary>
		/// <param name="logger">The logger service.</param>
		/// <param name="repository">The lazy-loaded repository.</param>
		protected DeleteService(ILoggerService logger, Lazy<TRepository> repository) : base(logger, repository)
		{
		}

		/// <summary>
		/// Asynchronously deletes an entity by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the entity to delete.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains true if the entity was deleted; otherwise, false.</returns>
		public virtual async Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken)
		{
			var deleted = await Repository.DeleteByIdAsync(id, cancellationToken);
			if (deleted) await Repository.SaveChangesAsync(cancellationToken);
			return deleted;
		}

		/// <summary>
		/// Asynchronously deletes a specific entity instance.
		/// </summary>
		/// <param name="entity">The entity to delete.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains true if the entity was deleted; otherwise, false.</returns>
		public virtual async Task<bool> DeleteAsync(TModel entity, CancellationToken cancellationToken)
		{
			var deleted = await Repository.DeleteAsync(entity, cancellationToken);
			if (deleted) await Repository.SaveChangesAsync(cancellationToken);
			return deleted;
		}
	}
}
