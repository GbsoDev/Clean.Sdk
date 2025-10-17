using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Ports;
using Microsoft.Extensions.Logging;

namespace Clean.Sdk.Domain.Services
{
	public abstract class SaveService<TModel, TRepository> : ActionService<TModel, TRepository>, ISaveService<TModel>
		where TModel : class, IDomainModel
		where TRepository : IRepository<TModel>
	{
		protected SaveService(ILogger<Service> logger, Lazy<TRepository> repository) : base(logger, repository)
		{
		}

		public virtual async Task<TModel> SaveAsync(TModel entity, CancellationToken cancellationToken)
		{
			var result = await Repository.SaveAsync(entity, cancellationToken).ConfigureAwait(false);
			await Repository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
			return result;
		}
	}
}
