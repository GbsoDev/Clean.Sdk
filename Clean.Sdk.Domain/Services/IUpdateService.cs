using Clean.Sdk.Domain.Entity;

namespace Clean.Sdk.Domain.Services
{
	public interface IUpdateService<TEntity>
		where TEntity : class, IDomainEntity
	{
		Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
	}
}
