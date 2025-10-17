using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Ports;
using Microsoft.Extensions.Logging;

namespace Clean.Sdk.Domain.Services
{
	public abstract class UpdateService<TModel, TRepository> : ActionService<TModel, TRepository>, IUpdateService<TModel>
		where TModel : class, IDomainModel
		where TRepository : IRepository<TModel>
	{
		protected UpdateService(ILogger<Service> logger, Lazy<TRepository> repository) : base(logger, repository)
		{
		}

		public async Task<TModel> UpdateAsync(TModel entity, CancellationToken cancellationToken)
		{
			var result = await Repository.UpdateAsync(entity, cancellationToken);
			await Repository.SaveChangesAsync(cancellationToken);
			return result;
		}
	}
}
