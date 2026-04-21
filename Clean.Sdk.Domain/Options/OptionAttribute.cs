namespace Clean.Sdk.Domain.Options
{
	/// <summary>
	/// Attribute used to mark classes that represent configuration options.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class OptionAttribute : Attribute
	{
		/// <summary>
		/// Gets the name of the section in the configuration file.
		/// </summary>
		public string? SecctionName { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="OptionAttribute"/> class.
		/// </summary>
		public OptionAttribute()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="OptionAttribute"/> class with a specified section name.
		/// </summary>
		/// <param name="secctionName">The name of the configuration section.</param>
		public OptionAttribute(string secctionName)
		{
			SecctionName = secctionName;
		}
	}
}
