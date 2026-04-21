using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Resources;

namespace Clean.Sdk.Domain.Options
{
	/// <summary>
	/// Represents the application settings.
	/// </summary>
	[Option()]
	public class AppSettings
	{
		/// <summary>
		/// Gets the dictionary of database connections.
		/// </summary>
		public Dictionary<string, DbConnection> DbConnections { get; private set; }

		/// <summary>
		/// Gets the authentication options.
		/// </summary>
		public AuthOptions AuthOptions { get; private set; }

		/// <summary>
		/// Gets the array of CORS configurations.
		/// </summary>
		public CorsOptions[] AllowCors { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AppSettings"/> class.
		/// </summary>
		public AppSettings()
		{
			AuthOptions = new AuthOptions();
			AllowCors = Array.Empty<CorsOptions>();
			DbConnections = new Dictionary<string, DbConnection>();
		}

		/// <summary>
		/// Gets a database connection by its name.
		/// </summary>
		/// <param name="name">The name of the database connection.</param>
		/// <returns>The <see cref="DbConnection"/> with the specified name.</returns>
		/// <exception cref="AppExeption">Thrown when a database connection with the specified name is not found.</exception>
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
