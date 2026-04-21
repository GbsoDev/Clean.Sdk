using AutoMapper;
using Clean.Sdk.Domain.Ports;

namespace Clean.Sdk.Application.Handlers
{
	/// <summary>
	/// Represents a base class for query handlers that interact with a repository.
	/// </summary>
	/// <typeparam name="TRepository">The type of the repository used by the handler.</typeparam>
	public abstract class QueryHandler<TRepository> : Handler
		where TRepository : class, IRepository
	{
		/// <summary>
		/// Gets the repository instance.
		/// </summary>
		protected TRepository Repository => _repository.Value;
		private readonly Lazy<TRepository> _repository;

		/// <summary>
		/// Initializes a new instance of the <see cref="QueryHandler{TRepository}"/> class.
		/// </summary>
		/// <param name="logger">The logger service.</param>
		/// <param name="mapper">The mapper service.</param>
		/// <param name="repository">The repository to be used by the handler.</param>
		public QueryHandler(Lazy<ILoggerService> logger, IMapper mapper, Lazy<TRepository> repository) : base(logger, mapper)
		{
			_repository = repository;
		}
	}
}
