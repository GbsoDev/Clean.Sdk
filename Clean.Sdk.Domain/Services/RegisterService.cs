using Clean.Sdk.Domain.Entity;
using Clean.Sdk.Domain.Ports;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Sdk.Domain.Services
{
	public abstract class RegisterService<TEntity, TRepository> : ActionService<TEntity, TRepository>, IRegisterService<TEntity>
		where TEntity : class, IDomainEntity
		where TRepository : IRepository<TEntity>
	{
		protected RegisterService(ILogger<Service> logger, Lazy<TRepository> repository) : base(logger, repository)
		{
		}

		public virtual async Task<TEntity> RegisterAsync(TEntity entity, CancellationToken cancellationToken)
		{
			var resultado = await Repository.StoreAsync(entity, cancellationToken);
			await Repository.SaveChangesAsync(cancellationToken);
			return resultado;
		}
	}
}
