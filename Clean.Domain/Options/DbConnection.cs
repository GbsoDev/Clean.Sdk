namespace Clean.Domain.Options
{
	public class DbConnection
	{
		public DbType DbType { get; set; }
		public string ConnectionString { get; set; }

		public DbConnection()
		{
			DbType = 0;
			ConnectionString = string.Empty;
		}

		public DbConnection(DbType dbType, string connectionString)
		{
			DbType = dbType;
			ConnectionString = connectionString;
		}
	}
}
