using AutoMapper;
using Clean.Sdk.Domain.Ports;

namespace Clean.Sdk.Application.Handlers
{
	public abstract class Handler
	{
		protected ILoggerService Logger => _logger.Value;
		private readonly Lazy<ILoggerService> _logger;
		protected IMapper Mapper { get; }

		protected Handler(Lazy<ILoggerService> logger, IMapper mapper)
		{
			_logger = logger;
			Mapper = mapper;
		}
	}
}
