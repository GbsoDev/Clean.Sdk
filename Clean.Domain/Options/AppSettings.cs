using System.Collections.Generic;

namespace Clean.Domain.Options
{
	public class AppSettings
	{
		public DbType dbType { get; set; }
		public Dictionary<string, string> ConnectionStrings { get; set; }
		public AuthOptions AuthOptions { get; set; }
		public CorsOptions[] AllowCors { get; set; }

		public AppSettings()
		{
			AuthOptions = new AuthOptions();
			AllowCors = new CorsOptions[0];
			ConnectionStrings = new Dictionary<string, string>();
		}

		public string? GetConnectionString(string connection)
		{
			return ConnectionStrings[connection];
		}
	}
}
