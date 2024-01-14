using Clean.Domain.Entity;
using Clean.Domain.Ports;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Domain.Services
{
	public abstract class UpdateService<TEntity, TRepository> : ActionService<TEntity, TRepository>, IUpdateService<TEntity>
		where TEntity : class, IDomainEntity
		where TRepository : IRepository<TEntity>
	{
		protected UpdateService(ILogger<Service> logger, Lazy<TRepository> repository) : base(logger, repository)
		{
		}

		public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			var resultado = await Repository.UpdateAsync(entity, cancellationToken);
			await Repository.SaveChangesAsync(cancellationToken);
			return resultado;
		}
	}
}
