namespace Clean.Sdk.Domain.Services
{
	/// <summary>
	/// Attribute used to mark classes as domain services for automatic discovery and registration.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ServiceAttribute : Attribute
	{
	}
}
