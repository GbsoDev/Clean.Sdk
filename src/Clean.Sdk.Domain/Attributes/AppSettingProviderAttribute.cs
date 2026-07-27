namespace Clean.Sdk.Domain.Attributes
{
	/// <summary>
	/// Attribute used to mark classes that represent configuration options.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class AppSettingProviderAttribute : Attribute
	{
		/// <summary>
		/// Gets the name of the section in the configuration file.
		/// </summary>
		public string? SectionName { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AppSettingProviderAttribute"/> class.
		/// </summary>
		public AppSettingProviderAttribute()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AppSettingProviderAttribute"/> class with a specified section name.
		/// </summary>
		/// <param name="sectionName">The name of the configuration section.</param>
		public AppSettingProviderAttribute(string sectionName)
		{
			SectionName = sectionName;
		}
	}
}
