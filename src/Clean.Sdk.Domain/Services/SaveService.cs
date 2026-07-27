using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Ports;

namespace Clean.Sdk.Domain.Services
{
	/// <summary>
	/// Base class for services that handle saving domain models.
	/// </summary>
	/// <typeparam name="TModel">The type of the domain model.</typeparam>
	/// <typeparam name="TRepository">The type of the repository.</typeparam>
	public abstract class SaveService<TModel, TRepository> : ActionService<TModel, TRepository>, ISaveService<TModel>
		where TModel : class, IDomainModel
		where TRepository : IRepository<TModel>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SaveService{TModel, TRepository}"/> class.
		/// </summary>
		/// <param name="logger">The logger service.</param>
		/// <param name="repository">The lazy-loaded repository.</param>
		protected SaveService(ILoggerService logger, Lazy<TRepository> repository) : base(logger, repository)
		{
		}

		/// <summary>
		/// Asynchronously saves a new entity instance.
		/// </summary>
		/// <param name="entity">The entity to save.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the saved entity.</returns>
		public virtual async Task<TModel> SaveAsync(TModel entity, CancellationToken cancellationToken)
		{
			var result = await Repository.SaveAsync(entity, cancellationToken).ConfigureAwait(false);
			await Repository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
			return result;
		}
	}
}
