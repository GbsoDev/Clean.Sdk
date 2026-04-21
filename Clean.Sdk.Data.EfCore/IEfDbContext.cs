using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Clean.Sdk.Data.EfCore
{
	/// <summary>
	/// Interface for the Entity Framework database context.
	/// </summary>
	public interface IEfDbContext
	{
		/// <summary>
		/// The name of the property that stores the save date of an entity.
		/// </summary>
		public const string SAVE_DATE_PROPERTY_NAME = "SaveDate";

		/// <summary>
		/// The name of the property that stores the last update date of an entity.
		/// </summary>
		public const string LAST_UPDATE_PROPERTY_NAME = "LastUpdate";

		/// <summary>
		/// Provides access to database-related information and operations for this context.
		/// </summary>
		DatabaseFacade Database { get; }

		/// <summary>
		/// Provides access to information and operations for entity instances this context is tracking.
		/// </summary>
		ChangeTracker ChangeTracker { get; }

		/// <summary>
		/// Finds an entity with the given primary key values asynchronously.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity to find.</typeparam>
		/// <param name="keyValues">The values of the primary key for the entity to be found.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the entity found, or null.</returns>
		ValueTask<TEntity?> FindAsync<TEntity>(object?[]? keyValues, CancellationToken cancellationToken = default) where TEntity : class;

		/// <summary>
		/// Begins tracking the given entity, and any other reachable entities that are not already being tracked, in the Added state such that they will be inserted into the database when SaveChanges is called.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity to add.</typeparam>
		/// <param name="entity">The entity to add.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the EntityEntry for the entity.</returns>
		ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

		/// <summary>
		/// Begins tracking the given entities, and any other reachable entities that are not already being tracked, in the Added state such that they will be inserted into the database when SaveChanges is called.
		/// </summary>
		/// <param name="entities">The entities to add.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default);

		/// <summary>
		/// Creates a DbSet that can be used to query and save instances of TEntity.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity for which a set should be returned.</typeparam>
		/// <returns>A set for the given entity type.</returns>
		DbSet<TEntity> Set<TEntity>() where TEntity : class;

		/// <summary>
		/// Gets an EntityEntry for the given entity. The entry provides access to change tracking information and operations for the entity.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="entity">The entity to get the entry for.</param>
		/// <returns>The entry for the given entity.</returns>
		EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

		/// <summary>
		/// Begins tracking the given entity in the Deleted state such that it will be removed from the database when SaveChanges is called.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="entity">The entity to remove.</param>
		/// <returns>The entry for the given entity.</returns>
		EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;

		/// <summary>
		/// Begins tracking the given entities in the Deleted state such that they will be removed from the database when SaveChanges is called.
		/// </summary>
		/// <param name="entities">The entities to remove.</param>
		void RemoveRange(IEnumerable<object> entities);

		/// <summary>
		/// Saves all changes made in this context to the database asynchronously.
		/// </summary>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Saves all changes made in this context to the database asynchronously.
		/// </summary>
		/// <param name="acceptAllChangesOnSuccess">Indicates whether AcceptAllChanges is called after the changes have been sent successfully to the database.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
		Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
	}
}
