using Clean.Sdk.Domain.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Sdk.Domain.Services
{
	public interface ISaveService<TEntity>
		where TEntity : class, IDomainEntity
	{
		Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken);
	}
}
