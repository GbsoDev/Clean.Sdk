using Clean.Domain.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Domain.Services
{
	public interface IRegisterService<TEntity>
		where TEntity : class, IDomainEntity
	{
		Task<TEntity> RegisterAsync(TEntity entity, CancellationToken cancellationToken);
	}
}
