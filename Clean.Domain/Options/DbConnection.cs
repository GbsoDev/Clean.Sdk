namespace Clean.Domain.Options
{
	public class DbConnection
	{
		public virtual DbType DbType { get; private set; }
		public virtual string ConnectionString { get; private set; }

		public DbConnection()
		{
			DbType = 0;
			ConnectionString = string.Empty;
		}
	}
}
