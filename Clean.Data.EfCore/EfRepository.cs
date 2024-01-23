﻿using Clean.Domain.Entity;
using Clean.Domain.Ports;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clean.Data.EfCore
{
	public class EfRepository<TEntity, TContext> : IRepository<TEntity>
		where TEntity : class, IDomainEntity
		where TContext : IEfDbContext
	{
		protected TContext Context { get; }

		public EfRepository(TContext context)
		{
			Context = context;
		}

		public virtual async Task<TEntity> StoreAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
			return entity;
		}

		public virtual Task<TEntity[]> ConsultAllAsync(CancellationToken cancellationToken = default)
		{
			return Context.Set<TEntity>().ToArrayAsync(cancellationToken);
		}

		public virtual async Task<TEntity?> ConsultByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			var keyValues = new object[] { id };
			return await Context.FindAsync<TEntity>(keyValues, cancellationToken);
		}

		public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			var entityResult = await ConsultByIdAsync(entity.Id, cancellationToken);
			if (entityResult == null) throw new ApplicationException("You are trying to update a record that does not exist");
			Context.Entry(entityResult).CurrentValues.SetValues(entity);
			return entityResult;
		}

		public virtual async Task<TEntity> UpdateAsync(TEntity entity, Expression<Func<TEntity, object>> @object, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			if (@object == null) throw new ArgumentNullException(nameof(@object));
			var entityResult = await ConsultByIdAsync(entity.Id, cancellationToken);
			if (entityResult == null) throw new ApplicationException("You are trying to update a record that does not exist");
			var objectResult = @object?.Compile()?.Invoke(entity);
			if (entityResult != null && objectResult != null)
				Context.Entry(entity).CurrentValues.SetValues(objectResult);
			return entityResult!;
		}

		public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			await Task.FromResult(Context.Remove(entity));
		}

		public virtual async Task DeleteByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			var entity = await ConsultByIdAsync(id, cancellationToken);
			if (entity == null) throw new ApplicationException("You are trying to delete a record that does not exist");
			await DeleteAsync(entity!, cancellationToken);
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			Context.ChangeTracker.DetectChanges();
			foreach (var entry in Context.ChangeTracker.Entries()
				.Where(entity => entity.State == EntityState.Modified))
			{
				entry.Property(IEfDbContext.LAST_UPDATE_PROPERTY_NAME).CurrentValue = DateTime.UtcNow;
			}
			await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
		}
	}
}