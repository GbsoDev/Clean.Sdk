using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Ports;

namespace Clean.Sdk.Domain.Services
{
	/// <summary>
	/// Base class for services that perform actions on a domain model using a repository.
	/// </summary>
	/// <typeparam name="TModel">The type of the domain model.</typeparam>
	/// <typeparam name="TRepository">The type of the repository.</typeparam>
	public abstract class ActionService<TModel, TRepository> : Service
		where TModel : class, IDomainModel
		where TRepository : IRepository<TModel>
	{
		/// <summary>
		/// Gets the repository instance.
		/// </summary>
		protected TRepository Repository => _repository.Value;
		private readonly Lazy<TRepository> _repository;

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionService{TModel, TRepository}"/> class.
		/// </summary>
		/// <param name="logger">The logger service.</param>
		/// <param name="repository">The lazy-loaded repository.</param>
		protected ActionService(ILoggerService logger, Lazy<TRepository> repository) : base(logger)
		{
			_repository = repository;
		}
	}
}
