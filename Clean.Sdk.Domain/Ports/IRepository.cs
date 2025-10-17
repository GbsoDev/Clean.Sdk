using Clean.Sdk.Domain.Model;

namespace Clean.Sdk.Domain.Ports
{
	public interface IRepository
	{
		Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken = default);
		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}

	public interface IRepository<TModel> : IRepository
		where TModel : class, IDomainModel
	{
		Task<TModel> SaveAsync(TModel model, CancellationToken cancellationToken = default);

		Task<TModel[]> GetAllAsync(CancellationToken cancellationToken = default);

		Task<TModel?> GetByIdAsync(object id, CancellationToken cancellationToken = default);

		Task<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken = default);

		Task<bool> DeleteAsync(TModel model, CancellationToken cancellationToken = default);
	}
}
