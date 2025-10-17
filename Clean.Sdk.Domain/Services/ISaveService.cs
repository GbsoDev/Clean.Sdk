using Clean.Sdk.Domain.Model;

namespace Clean.Sdk.Domain.Services
{
	public interface ISaveService<TModel>
		where TModel : class, IDomainModel
	{
		Task<TModel> SaveAsync(TModel entity, CancellationToken cancellationToken);
	}
}
