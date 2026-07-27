namespace Clean.Sdk.Domain.Ports
{
	/// <summary>
	/// Defines a service for logging messages with different severity levels.
	/// </summary>
	public interface ILoggerService
	{
		/// <summary>
		/// Logs an informational message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		void LogInformation(string message);

		/// <summary>
		/// Logs a warning message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		void LogWarning(string message);

		/// <summary>
		/// Logs an error message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <param name="ex">The exception to log, if any.</param>
		void LogError(string message, Exception ex = null);

		/// <summary>
		/// Logs a critical message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		/// <param name="ex">The exception to log, if any.</param>
		void LogCritical(string message, Exception ex = null);
	}
}
