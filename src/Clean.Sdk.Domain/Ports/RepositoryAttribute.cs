namespace Clean.Sdk.Domain.Ports
{
	/// <summary>
	/// Attribute used to mark classes that implement the repository pattern for automatic discovery and registration.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class RepositoryAttribute : Attribute
	{
	}
}
