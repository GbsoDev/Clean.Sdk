using Clean.Sdk.Domain.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Sdk.Domain.Services
{
	public interface IUpdateService<TEntity>
		where TEntity : class, IDomainEntity
	{
		Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
	}
}
