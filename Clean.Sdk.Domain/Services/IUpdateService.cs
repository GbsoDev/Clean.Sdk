using Clean.Sdk.Domain.Model;

namespace Clean.Sdk.Domain.Services
{
	public interface IUpdateService<TModel>
		where TModel : class, IDomainModel
	{
		Task<TModel> UpdateAsync(TModel entity, CancellationToken cancellationToken);
	}
}
