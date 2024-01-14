using Clean.Domain.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Domain.Services
{
	internal interface ICrudService<TEntity>
		where TEntity : class, IDomainEntity
	{
		Task DeleteByIdAsync(object id, CancellationToken cancellationToken = default);
		Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
		Task<TEntity[]> LisAsync(CancellationToken cancellationToken = default);
		Task<TEntity> RegisterAsync(TEntity entity, CancellationToken cancellationToken = default);
		Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
	}
}