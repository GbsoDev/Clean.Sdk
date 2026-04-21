namespace Clean.Sdk.Domain.Options
{
	/// <summary>
	/// Represents a database connection configuration.
	/// </summary>
	public class DbConnection
	{
		/// <summary>
		/// Gets the type of the database.
		/// </summary>
		public virtual DbType DbType { get; private set; }

		/// <summary>
		/// Gets the connection string for the database.
		/// </summary>
		public virtual string ConnectionString { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DbConnection"/> class.
		/// </summary>
		public DbConnection()
		{
			DbType = 0;
			ConnectionString = string.Empty;
		}
	}
}
