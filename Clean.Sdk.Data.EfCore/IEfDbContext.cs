using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Clean.Sdk.Data.EfCore
{
	public interface IEfDbContext
	{
		public static string REGISTER_DATE_PROPERTY_NAME = "RegisterDate";
		public static string LAST_UPDATE_PROPERTY_NAME = "LastUpdate";

		DatabaseFacade Database { get; }
		ChangeTracker ChangeTracker { get; }

		ValueTask<TEntity?> FindAsync<TEntity>(object?[]? keyValues, CancellationToken cancellationToken = default) where TEntity : class;

		ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

		Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default);

		DbSet<TEntity> Set<TEntity>() where TEntity : class;

		EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

		EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;

		void RemoveRange(IEnumerable<object> entities);

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

		Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
	}
}
