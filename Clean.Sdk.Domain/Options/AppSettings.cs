using Clean.Domain.Exceptions;
using System.Collections.Generic;

namespace Clean.Domain.Options
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
			AllowCors = new CorsOptions[0];
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
