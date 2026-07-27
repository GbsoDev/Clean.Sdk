namespace Clean.Sdk.Domain.Ports
{
	/// <summary>
	/// Defines a provider for date and time information to allow for easier testing and abstraction of time-dependent logic.
	/// </summary>
	public interface IDateTimeProvider
	{
		/// <summary>
		/// Gets the local time zone information.
		/// </summary>
		TimeZoneInfo localTimeZone { get; }

		/// <summary>
		/// Gets the current date and time in Coordinated Universal Time (UTC).
		/// </summary>
		DateTime UtcNow { get; }

		/// <summary>
		/// Gets the current local date and time.
		/// </summary>
		DateTime Now { get; }

		/// <summary>
		/// Gets the current date.
		/// </summary>
		DateTime Today { get; }
	}
}
