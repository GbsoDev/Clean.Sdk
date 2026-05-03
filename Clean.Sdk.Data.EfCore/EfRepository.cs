using AutoMapper;
using Clean.Sdk.Data.Entities;
using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Clean.Sdk.Data.EfCore
{
	/// <summary>
	/// Generic implementation of the repository pattern using Entity Framework Core.
	/// </summary>
	/// <typeparam name="TModel">The type of the domain model.</typeparam>
	/// <typeparam name="TEntity">The type of the database entity.</typeparam>
	/// <typeparam name="TContext">The type of the database context.</typeparam>
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

		/// <summary>
		/// Initializes a new instance of the <see cref="EfRepository{TModel, TEntity, TContext}"/> class.
		/// </summary>
		/// <param name="context">The database context.</param>
		/// <param name="mapper">The mapper service.</param>
		/// <param name="dateTimeProvider">The date and time provider service.</param>
		public EfRepository(TContext context, Lazy<IMapper> mapper, Lazy<IDateTimeProvider> dateTimeProvider)
		{
			Context = context;
			_mapper = mapper;
			_dateTimeProvider = dateTimeProvider;
		}


		/// <summary>
		/// Saves a model to the database asynchronously.
		/// </summary>
		/// <param name="model">The model to save.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the saved model.</returns>
		public virtual async Task<TModel> SaveAsync(TModel model, CancellationToken cancellationToken = default)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));
			TEntity entity = Mapper.Map<TEntity>(model);
			await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
			return entity;
		}

		/// <summary>
		/// Gets all models from the database asynchronously.
		/// </summary>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains an array of models.</returns>
		public virtual async Task<TModel[]> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await Context.Set<TEntity>().ToArrayAsync(cancellationToken);
		}



		/// <summary>
		/// Gets a model by its identifier asynchronously.
		/// </summary>
		/// <param name="id">The identifier of the model.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the model if found; otherwise, null.</returns>
		public virtual async Task<TModel?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			return await GetEntityByIdAsync(id, cancellationToken);
		}

		/// <summary>
		/// Updates a model in the database asynchronously.
		/// </summary>
		/// <param name="model">The model to update.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the updated model.</returns>
		/// <exception cref="NotFoundException">Thrown when the model is not found in the database.</exception>
		public virtual async Task<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken = default)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));
			TEntity? entityResult = await GetEntityByIdAsync(model.Id, cancellationToken);
			if (entityResult == null) throw new NotFoundException(Messages.NotFoundExcepton, model.GetType().Name);
			Context.Entry(entityResult).CurrentValues.SetValues(model);
			return entityResult;
		}

		/// <summary>
		/// Updates a specific object within a model in the database asynchronously.
		/// </summary>
		/// <param name="model">The model containing the object to update.</param>
		/// <param name="object">An expression identifying the object to update.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the updated model.</returns>
		/// <exception cref="NotFoundException">Thrown when the model is not found in the database.</exception>
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

		/// <summary>
		/// Deletes a model from the database asynchronously.
		/// </summary>
		/// <param name="model">The model to delete.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result is true if the model was deleted; otherwise, false.</returns>
		public virtual async Task<bool> DeleteAsync(TModel model, CancellationToken cancellationToken = default)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));
			TEntity entity = Mapper.Map<TEntity>(model);
            EntityEntry<TEntity> entityEntry = Context.Set<TEntity>().Remove(entity);
			return await Task.FromResult(entityEntry?.State == EntityState.Deleted);
		}

		/// <summary>
		/// Deletes a model by its identifier asynchronously.
		/// </summary>
		/// <param name="id">The identifier of the model to delete.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result is true if the model was deleted; otherwise, false.</returns>
		public virtual async Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			TEntity? entity = await GetEntityByIdAsync(id, cancellationToken);

			if (entity == null) return false;
            return await DeleteAsync(entity!, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Saves all changes made in this context to the database asynchronously.
		/// </summary>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
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