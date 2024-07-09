using Clean.Data.EfCore;
using Clean.Domain.Exceptions;
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

		public static IServiceCollection AddEfCoreContext<TContext, TImplementarion>(this IServiceCollection services, DbConnection dbConecction)
			where TContext : class
			where TImplementarion : EfDbContext<TImplementarion>, TContext
		{
			switch (dbConecction.DbType)
			{
				case DbType.MSSQL:
					services.AddDbContext<TImplementarion>(options => options.UseSqlServer(dbConecction.ConnectionString));
					break;
				case DbType.MySql:
					services.AddDbContext<TImplementarion>(options => options.UseMySQL(dbConecction.ConnectionString));
					break;
				case DbType.PostgreSql:
					services.AddDbContext<TImplementarion>(options => options.UseNpgsql(dbConecction.ConnectionString));
					break;
				case DbType.InMemory:
					services.AddDbContext<TImplementarion>(options => options.UseInMemoryDatabase(dbConecction.ConnectionString));
					break;
				default:
					break;
			}

			return services.AddScoped<TContext, TImplementarion>();
		}
	}
}
