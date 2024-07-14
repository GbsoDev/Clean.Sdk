using Clean.Sdk.Domain.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Sdk.Domain.Services
{
	public interface IDeleteService<TEntity>
		where TEntity : class, IDomainEntity
	{
		Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken);
		Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken);
	}
}
