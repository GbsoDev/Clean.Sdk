using Clean.Data.EfCore;
using Clean.Domain.Exceptions;
using Clean.Domain.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Infrastructure.Extensions
{
	public static class EfCoreProvider
	{
		public static IServiceProvider MigrateDataBase(this IServiceProvider service, AppSettings appSettings)
		{
			switch (appSettings.dbType)
			{
				case DbType.MSSQL:
				case DbType.MySql:
				case DbType.PostgreSql:
					using (var scope = service.CreateScope())
					{
						var services = scope.ServiceProvider;
						try
						{
							var dbContext = services.GetRequiredService<IEfDbContext>();
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
	}
}
