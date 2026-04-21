namespace Clean.Sdk.Domain.Options
{
	/// <summary>
	/// Specifies the supported database types.
	/// </summary>
	public enum DbType : short
	{
		/// <summary>
		/// Microsoft SQL Server.
		/// </summary>
		MSSQL = 1,

		/// <summary>
		/// MySQL Server.
		/// </summary>
		MySql = 2,

		/// <summary>
		/// PostgreSQL Server.
		/// </summary>
		PostgreSql = 3,

		/// <summary>
		/// In-memory database (primarily for testing).
		/// </summary>
		InMemory = 4
	}
}
