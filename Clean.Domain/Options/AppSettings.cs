using System.Collections.Generic;

namespace Clean.Domain.Options
{
	public class AppSettings
	{
		public Dictionary<string, string> ConnectionStrings { get; set; }

		public AppSettings()
		{
			ConnectionStrings = new Dictionary<string, string>();
		}

		public string? GetConnectionString(string connection)
		{
			return ConnectionStrings[connection];
		}
	}
}
