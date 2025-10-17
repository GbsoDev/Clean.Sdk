using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Ports;
using Microsoft.Extensions.Logging;

namespace Clean.Sdk.Domain.Services
{
	public abstract class ActionService<TModel, TRepository> : Service
		where TModel : class, IDomainModel
		where TRepository : IRepository<TModel>
	{
		protected TRepository Repository => _repository.Value;
		private readonly Lazy<TRepository> _repository;

		protected ActionService(ILogger<Service> logger, Lazy<TRepository> repository) : base(logger)
		{
			_repository = repository;
		}
	}
}
