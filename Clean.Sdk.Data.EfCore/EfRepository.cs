using AutoMapper;
using Clean.Sdk.Data.EfCore.Entities;
using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Clean.Sdk.Data.EfCore
{
	public class EfRepository<TModel, TEntity, TContext> : IRepository<TModel>
		where TModel : class, Domain.Model.IDomainModel
		where TEntity : class, IDomainEntity, TModel
		where TContext : IEfDbContext
	{

		protected TContext Context { get; }
		protected IMapper Mapper { get => _mapper.Value; }
		private readonly Lazy<IMapper> _mapper;
		protected IDateTimeProvider DateTimeProvider => _dateTimeProvider.Value;
		private readonly Lazy<IDateTimeProvider> _dateTimeProvider;

		public EfRepository(TContext context, Lazy<IMapper> mapper, Lazy<IDateTimeProvider> dateTimeProvider)
		{
			Context = context;
			_mapper = mapper;
			_dateTimeProvider = dateTimeProvider;
		}


		public virtual async Task<TModel> SaveAsync(TModel model, CancellationToken cancellationToken = default)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));
			TEntity entity = Mapper.Map<TEntity>(model);
			await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
			return entity;
		}

		public virtual async Task<TModel[]> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await Context.Set<TEntity>().ToArrayAsync(cancellationToken);
		}



		public virtual async Task<TModel?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			return await GetEntityByIdAsync(id, cancellationToken);
		}

		public virtual async Task<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken = default)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));
			TEntity? entityResult = await GetEntityByIdAsync(model.Id, cancellationToken);
			if (entityResult == null) throw new NotFoundException(Messages.NotFoundExcepton, model.GetType().Name);
			Context.Entry(entityResult).CurrentValues.SetValues(model);
			return entityResult;
		}

		public virtual async Task<TModel> UpdateAsync(TModel model, Expression<Func<TModel, object>> @object, CancellationToken cancellationToken = default)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));
			if (@object == null) throw new ArgumentNullException(nameof(@object));
			TEntity? entityResult = await GetEntityByIdAsync(model.Id, cancellationToken);
			if (entityResult == null) throw new NotFoundException("You are trying to update a record that does not exist");
			object? objectResult = @object?.Compile()?.Invoke(model);
			if (entityResult != null && objectResult != null)
				Context.Entry(model).CurrentValues.SetValues(objectResult);
			return entityResult!;
		}

		public virtual async Task<bool> DeleteAsync(TModel model, CancellationToken cancellationToken = default)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));
			TEntity entity = Mapper.Map<TEntity>(model);
            EntityEntry<TEntity> entityEntry = Context.Set<TEntity>().Remove(entity);
			return await Task.FromResult(entityEntry?.State == EntityState.Deleted);
		}

		public virtual async Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			TEntity? entity = await GetEntityByIdAsync(id, cancellationToken);

			if (entity == null) return false;
            return await DeleteAsync(entity!, cancellationToken).ConfigureAwait(false);
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			Context.ChangeTracker.DetectChanges();
			foreach (EntityEntry entry in Context.ChangeTracker.Entries())
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

		private async Task<TEntity?> GetEntityByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			object[] keyValues = new object[] { id };
			return await Context.FindAsync<TEntity>(keyValues, cancellationToken);
		}
	}
}