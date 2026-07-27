using Clean.Sdk.Domain.Ports;

namespace Clean.Sdk.Domain.Services
{
	/// <summary>
	/// Base class for all domain services.
	/// </summary>
	public abstract class Service
	{
		/// <summary>
		/// The logger service instance.
		/// </summary>
		protected readonly ILoggerService Logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="Service"/> class.
		/// </summary>
		/// <param name="logger">The logger service.</param>
		protected Service(ILoggerService logger)
		{
			Logger = logger;
		}
	}
}
