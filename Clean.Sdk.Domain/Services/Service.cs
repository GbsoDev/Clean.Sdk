using Clean.Sdk.Domain.Ports;

namespace Clean.Sdk.Domain.Services
{
	public abstract class Service
	{
		protected readonly ILoggerService Logger;

		protected Service(ILoggerService logger)
		{
			Logger = logger;
		}
	}
}
