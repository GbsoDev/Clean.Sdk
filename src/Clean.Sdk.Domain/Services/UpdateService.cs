using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Ports;

namespace Clean.Sdk.Domain.Services
{
	/// <summary>
	/// Base class for services that handle updating domain models.
	/// </summary>
	/// <typeparam name="TModel">The type of the domain model.</typeparam>
	/// <typeparam name="TRepository">The type of the repository.</typeparam>
	public abstract class UpdateService<TModel, TRepository> : ActionService<TModel, TRepository>, IUpdateService<TModel>
		where TModel : class, IDomainModel
		where TRepository : IRepository<TModel>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UpdateService{TModel, TRepository}"/> class.
		/// </summary>
		/// <param name="logger">The logger service.</param>
		/// <param name="repository">The lazy-loaded repository.</param>
		protected UpdateService(ILoggerService logger, Lazy<TRepository> repository) : base(logger, repository)
		{
		}

		/// <summary>
		/// Asynchronously updates an existing entity instance.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
		public async Task<TModel> UpdateAsync(TModel entity, CancellationToken cancellationToken)
		{
			var result = await Repository.UpdateAsync(entity, cancellationToken);
			await Repository.SaveChangesAsync(cancellationToken);
			return result;
		}
	}
}
