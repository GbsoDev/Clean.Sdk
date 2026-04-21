namespace Clean.Sdk.Application.Handlers
{
	/// <summary>
	/// Attribute used to mark classes as application handlers for automatic registration.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class AplicacionHandlerAttribute : Attribute
	{
	}
}