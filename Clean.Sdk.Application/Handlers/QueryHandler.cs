using AutoMapper;
using Clean.Sdk.Domain.Ports;

namespace Clean.Sdk.Application.Handlers
{
	public abstract class QueryHandler<TRepository> : Handler
		where TRepository : class, IRepository
	{
		protected TRepository Repository => _repository.Value;
		private readonly Lazy<TRepository> _repository;

		public QueryHandler(Lazy<ILoggerService> logger, IMapper mapper, Lazy<TRepository> repository) : base(logger, mapper)
		{
			_repository = repository;
		}
	}
}
