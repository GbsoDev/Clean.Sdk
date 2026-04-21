using Clean.Sdk.Data.EfCore;
using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Clean.Sdk.Infrastructure.Extensions
{
	/// <summary>
	/// Provider for Entity Framework Core configurations and operations.
	/// </summary>
	public static class EfCoreProvider
	{
		/// <summary>
		/// Migrates the database for the specified context type.
		/// </summary>
		/// <typeparam name="TDbContext">The type of the database context.</typeparam>
		/// <param name="service">The service provider.</param>
		/// <param name="dbConnection">The database connection configuration.</param>
		/// <returns>The service provider.</returns>
		/// <exception cref="AppExeption">Thrown when an error occurs during migration.</exception>
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

		/// <summary>
		/// Adds Entity Framework Core context to the service collection.
		/// </summary>
		/// <typeparam name="TContext">The interface type of the context.</typeparam>
		/// <typeparam name="TImplementarion">The implementation type of the context.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="dbConecction">The database connection configuration.</param>
		/// <returns>The service collection.</returns>
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
