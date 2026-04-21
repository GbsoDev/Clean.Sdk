using AutoMapper;
using Clean.Sdk.Domain.Ports;

namespace Clean.Sdk.Application.Handlers
{
	/// <summary>
	/// Represents the base class for all application handlers, providing common services like logging and mapping.
	/// </summary>
	public abstract class Handler
	{
		/// <summary>
		/// Gets the logger service.
		/// </summary>
		protected ILoggerService Logger => _logger.Value;
		private readonly Lazy<ILoggerService> _logger;

		/// <summary>
		/// Gets the mapper service.
		/// </summary>
		protected IMapper Mapper { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Handler"/> class.
		/// </summary>
		/// <param name="logger">The logger service.</param>
		/// <param name="mapper">The mapper service.</param>
		protected Handler(Lazy<ILoggerService> logger, IMapper mapper)
		{
			_logger = logger;
			Mapper = mapper;
		}
	}
}
