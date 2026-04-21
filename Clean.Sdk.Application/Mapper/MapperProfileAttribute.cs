namespace Clean.Sdk.Application.Mapper
{
	/// <summary>
	/// Attribute used to mark classes as AutoMapper profiles for automatic registration.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class MapperProfileAttribute : Attribute
	{
	}
}