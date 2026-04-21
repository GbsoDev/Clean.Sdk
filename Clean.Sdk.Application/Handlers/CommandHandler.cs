using AutoMapper;
using Clean.Sdk.Domain.Ports;

namespace Clean.Sdk.Application.Handlers
{
	/// <summary>
	/// Represents a base class for command handlers that interact with a service.
	/// </summary>
	/// <typeparam name="TService">The type of the service used by the handler.</typeparam>
	public abstract class CommandHandler<TService> : Handler
		where TService : class
	{
		/// <summary>
		/// Gets the service instance.
		/// </summary>
		protected virtual TService Service => _service.Value;
		private readonly Lazy<TService> _service;

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandHandler{TService}"/> class.
		/// </summary>
		/// <param name="logger">The logger service.</param>
		/// <param name="mapper">The mapper service.</param>
		/// <param name="service">The service to be used by the handler.</param>
		protected CommandHandler(Lazy<ILoggerService> logger, IMapper mapper, Lazy<TService> service) : base(logger, mapper)
		{
			_service = service;
		}
	}
}
