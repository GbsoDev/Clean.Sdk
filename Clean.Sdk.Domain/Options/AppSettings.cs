using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Resources;

namespace Clean.Sdk.Domain.Options
{
	[Option()]
	public class AppSettings
	{
		public Dictionary<string, DbConnection> DbConnections { get; private set; }
		public AuthOptions AuthOptions { get; private set; }
		public CorsOptions[] AllowCors { get; private set; }

		public AppSettings()
		{
			AuthOptions = new AuthOptions();
			AllowCors = Array.Empty<CorsOptions>();
			DbConnections = new Dictionary<string, DbConnection>();
		}

		public DbConnection GetConnection(string name)
		{
			try
			{
				return DbConnections[name];
			}
			catch (KeyNotFoundException)
			{
				throw new AppExeption(string.Format(Messages.DbConnectionNotFound, name));
			}
		}
	}
}
