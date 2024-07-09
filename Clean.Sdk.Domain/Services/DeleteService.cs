using Clean.Sdk.Domain.Entity;
using Clean.Sdk.Domain.Ports;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Sdk.Domain.Services
{
	public abstract class DeleteService<TEntity, TRepository> : ActionService<TEntity, TRepository>, IDeleteService<TEntity>
		where TEntity : class, IDomainEntity
		where TRepository : IRepository<TEntity>
	{
		protected DeleteService(ILogger<Service> logger, Lazy<TRepository> repository) : base(logger, repository)
		{
		}

		public virtual async Task DeleteByIdAsync(object id, CancellationToken cancellationToken)
		{
			await Repository.DeleteByIdAsync(id, cancellationToken);
			await Repository.SaveChangesAsync(cancellationToken);
		}

		public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
		{
			await DeleteByIdAsync(entity.Id, cancellationToken);
		}
	}
}
