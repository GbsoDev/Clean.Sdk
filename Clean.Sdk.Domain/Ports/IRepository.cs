using Clean.Sdk.Domain.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Sdk.Domain.Ports
{
	public interface IRepository
	{
		Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken = default);
		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}
	public interface IRepository<TEntity> : IRepository
		where TEntity : class, IDomainEntity
	{
		Task<TEntity> StoreAsync(TEntity entity, CancellationToken cancellationToken = default);

		Task<TEntity[]> ConsultAllAsync(CancellationToken cancellationToken = default);

		Task<TEntity?> ConsultByIdAsync(object id, CancellationToken cancellationToken = default);

		Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

		Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
	}
}
