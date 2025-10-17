using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Ports;
using Microsoft.Extensions.Logging;

namespace Clean.Sdk.Domain.Services
{
	public abstract class DeleteService<TModel, TRepository> : ActionService<TModel, TRepository>, IDeleteService<TModel>
		where TModel : class, IDomainModel
		where TRepository : IRepository<TModel>
	{
		protected DeleteService(ILogger<Service> logger, Lazy<TRepository> repository) : base(logger, repository)
		{
		}

		public virtual async Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken)
		{
			var deleted = await Repository.DeleteByIdAsync(id, cancellationToken);
			if (deleted) await Repository.SaveChangesAsync(cancellationToken);
			return deleted;
		}

		public virtual async Task<bool> DeleteAsync(TModel entity, CancellationToken cancellationToken)
		{
			var deleted = await Repository.DeleteAsync(entity, cancellationToken);
			if (deleted) await Repository.SaveChangesAsync(cancellationToken);
			return deleted;
		}
	}
}
