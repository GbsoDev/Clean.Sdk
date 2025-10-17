using Clean.Sdk.Domain.Entity;

namespace Clean.Sdk.Domain.Services
{
	internal interface ICrudService<TEntity>
		where TEntity : class, IDomainEntity
	{
		Task DeleteByIdAsync(object id, CancellationToken cancellationToken = default);
		Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
		Task<TEntity[]> LisAsync(CancellationToken cancellationToken = default);
		Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
		Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
	}
}