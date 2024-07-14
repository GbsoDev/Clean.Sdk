using Clean.Sdk.Domain.Entity;
using Clean.Sdk.Domain.Ports;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Sdk.Domain.Services
{
	public abstract class SaveService<TEntity, TRepository> : ActionService<TEntity, TRepository>, ISaveService<TEntity>
		where TEntity : class, IDomainEntity
		where TRepository : IRepository<TEntity>
	{
		protected SaveService(ILogger<Service> logger, Lazy<TRepository> repository) : base(logger, repository)
		{
		}

		public virtual async Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken)
		{
			var result = await Repository.SaveAsync(entity, cancellationToken).ConfigureAwait(false);
			await Repository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
			return result;
		}
	}
}
