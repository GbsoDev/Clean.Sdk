using Clean.Sdk.Domain.Entity;
using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clean.Sdk.Data.EfCore
{
	public class EfRepository<TEntity, TContext> : IRepository<TEntity>
		where TEntity : class, IDomainEntity
		where TContext : IEfDbContext
	{

		protected TContext Context { get; }
		private IDateTimeProvider DateTimeProvider => _dateTimeProvider.Value;
		private readonly Lazy<IDateTimeProvider> _dateTimeProvider;

		public EfRepository(TContext context, Lazy<IDateTimeProvider> dateTimeProvider)
		{
			Context = context;
			this._dateTimeProvider = dateTimeProvider;
		}

		public virtual async Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
			return entity;
		}

		public virtual Task<TEntity[]> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return Context.Set<TEntity>().ToArrayAsync(cancellationToken);
		}

		public virtual Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			var keyValues = new object[] { id };
			return Context.FindAsync<TEntity>(keyValues, cancellationToken).AsTask();
		}

		public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var entityResult = await GetByIdAsync(entity.Id, cancellationToken);
			if (entityResult == null) throw new NotFoundException(Messages.NotFoundExcepton, entity.GetType().Name);
			Context.Entry(entityResult).CurrentValues.SetValues(entity);
			return entityResult;
		}

		public virtual async Task<TEntity> UpdateAsync(TEntity entity, Expression<Func<TEntity, object>> @object, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			if (@object == null) throw new ArgumentNullException(nameof(@object));
			var entityResult = await GetByIdAsync(entity.Id, cancellationToken);
			if (entityResult == null) throw new NotFoundException("You are trying to update a record that does not exist");
			var objectResult = @object?.Compile()?.Invoke(entity);
			if (entityResult != null && objectResult != null)
				Context.Entry(entity).CurrentValues.SetValues(objectResult);
			return entityResult!;
		}

		public virtual async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var entityEntry = Context.Remove(entity);
			return await Task.FromResult(entityEntry.State == EntityState.Deleted);
		}

		public virtual async Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			var entity = await GetByIdAsync(id, cancellationToken);

			if (entity == null) return false;
			await DeleteAsync(entity!, cancellationToken).ConfigureAwait(false);
			return true;
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			Context.ChangeTracker.DetectChanges();
			foreach (var entry in Context.ChangeTracker.Entries())
			{
				if (entry.State == EntityState.Added)
				{
					entry.Property(IEfDbContext.SAVE_DATE_PROPERTY_NAME).CurrentValue = DateTimeProvider.UtcNow;
				}
				if (entry.State == EntityState.Modified)
				{
					entry.Property(IEfDbContext.LAST_UPDATE_PROPERTY_NAME).CurrentValue = DateTimeProvider.UtcNow;
				}
			}
			await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}
	}
}