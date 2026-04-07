namespace Clean.Sdk.Domain.Ports
{
	public interface ILoggerService
	{
		void LogInformation(string message);
		void LogWarning(string message);
		void LogError(string message, Exception ex = null);
		void LogCritical(string message, Exception ex = null);
	}
}
