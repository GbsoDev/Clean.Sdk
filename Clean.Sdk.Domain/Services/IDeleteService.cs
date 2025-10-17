using Clean.Sdk.Domain.Model;

namespace Clean.Sdk.Domain.Services
{
	public interface IDeleteService<TModel>
		where TModel : class, IDomainModel
	{
		Task<bool> DeleteAsync(TModel entity, CancellationToken cancellationToken);
		Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken);
	}
}
