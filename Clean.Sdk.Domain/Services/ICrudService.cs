using Clean.Sdk.Domain.Model;

namespace Clean.Sdk.Domain.Services
{
	internal interface ICrudService<TModel>
		where TModel : class, IDomainModel
	{
		Task DeleteByIdAsync(object id, CancellationToken cancellationToken = default);
		Task<TModel?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
		Task<TModel[]> LisAsync(CancellationToken cancellationToken = default);
		Task<TModel> SaveAsync(TModel entity, CancellationToken cancellationToken = default);
		Task<TModel> UpdateAsync(TModel entity, CancellationToken cancellationToken = default);
	}
}