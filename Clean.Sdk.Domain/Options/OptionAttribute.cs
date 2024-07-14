using System;

namespace Clean.Sdk.Domain.Options
{
	[AttributeUsage(AttributeTargets.Class)]
	public class OptionAttribute : Attribute
	{
		public string? SecctionName { get; }

		public OptionAttribute()
		{
		}

		public OptionAttribute(string secctionName)
		{
			SecctionName = secctionName;
		}
	}
}
