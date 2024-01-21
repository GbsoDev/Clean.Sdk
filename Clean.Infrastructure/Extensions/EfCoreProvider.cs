using Clean.Data.EfCore;
using Clean.Domain;
using Clean.Domain.Exceptions;
using Clean.Domain.Helpers;
using Clean.Domain.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Clean.Infrastructure.Extensions
{
	public static class EfCoreProvider
	{
		public static IServiceProvider MigrateDataBase<TDbContext>(this IServiceProvider service, DbConnection dbConnection)
			where TDbContext : IEfDbContext
		{
			switch (dbConnection.DbType)
			{
				case DbType.MSSQL:
				case DbType.MySql:
				case DbType.PostgreSql:
					using (var scope = service.CreateScope())
					{
						var services = scope.ServiceProvider;
						try
						{
							var dbContext = services.GetRequiredService<TDbContext>();
							dbContext.Database.Migrate();
						}
						catch (Exception ex)
						{
							throw new AppExeption("Error in migration", ex);
						}
					}
					break;
				default:
					break;
			}
			return service;
		}

		public static IServiceCollection AddEfCoreContext<TContext>(this IServiceCollection services, DbConnection dbConecction)
			where TContext : EfDbContext<TContext>
		{
			var type = typeof(TContext);
			var @interface = type.GetInterface(type.BuildInterfaceName());
			switch (dbConecction.DbType)
			{
				case DbType.MSSQL:
					services.AddDbContext<TContext>(options => options.UseSqlServer(dbConecction.ConnectionString));
					break;
				case DbType.MySql:
					services.AddDbContext<TContext>(options => options.UseMySQL(dbConecction.ConnectionString));
					break;
				case DbType.PostgreSql:
					services.AddDbContext<TContext>(options => options.UseNpgsql(dbConecction.ConnectionString));
					break;
				case DbType.InMemory:
					services.AddDbContext<TContext>(options => options.UseInMemoryDatabase(dbConecction.ConnectionString));
					break;
				default:
					break;
			}

			if (@interface != null)
			{
				services.AddScoped(@interface, type);
			}
			else
			{
				throw new AppExeption(string.Format(Messages.ServiceHasNoInterface, type.Name));
			}
			return services;
		}
	}
}
