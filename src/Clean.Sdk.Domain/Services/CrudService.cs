using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Ports;

namespace Clean.Sdk.Domain.Services
{
	/// <summary>
	/// Base class for CRUD (Create, Read, Update, Delete) operations on a domain model.
	/// </summary>
	/// <typeparam name="TModel">The type of the domain model.</typeparam>
	/// <typeparam name="TRepository">The type of the repository.</typeparam>
	[Obsolete("in construction", true)]
	public abstract class CrudService<TModel, TRepository> : ActionService<TModel, TRepository>, ICrudService<TModel>
		where TModel : class, IDomainModel
		where TRepository : IRepository<TModel>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CrudService{TModel, TRepository}"/> class.
		/// </summary>
		/// <param name="logger">The logger service.</param>
		/// <param name="repository">The lazy-loaded repository.</param>
		protected CrudService(ILoggerService logger, Lazy<TRepository> repository) : base(logger, repository)
		{
		}

		/// <summary>
		/// Asynchronously saves a new entity.
		/// </summary>
		/// <param name="entity">The entity to save.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the saved entity.</returns>
		/// <exception cref="ArgumentNullException">Thrown when the entity is null.</exception>
		public virtual async Task<TModel> SaveAsync(TModel entity, CancellationToken cancellationToken = default)
		{
			if (entity is null) throw new ArgumentNullException(nameof(entity));
			var modeloResgistrado = await Repository.SaveAsync(entity, cancellationToken);
			await Repository.SaveChangesAsync(cancellationToken);
			return modeloResgistrado;
		}

		/// <summary>
		/// Asynchronously retrieves all entities.
		/// </summary>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains an array of entities.</returns>
		public virtual async Task<TModel[]> LisAsync(CancellationToken cancellationToken = default)
		{
			var resultadoEntidades = await Repository.GetAllAsync(cancellationToken);
			return resultadoEntidades;
		}

		/// <summary>
		/// Asynchronously retrieves an entity by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the entity.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, null.</returns>
		public virtual async Task<TModel?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			var resultadoEntidad = await Repository.GetByIdAsync(id, cancellationToken);
			return resultadoEntidad;
		}

		/// <summary>
		/// Asynchronously updates an existing entity.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
		public virtual async Task<TModel> UpdateAsync(TModel entity, CancellationToken cancellationToken = default)
		{
			var resultadoEntidadActualizada = await Repository.UpdateAsync(entity, cancellationToken);
			await Repository.SaveChangesAsync(cancellationToken);
			return resultadoEntidadActualizada;
		}

		/// <summary>
		/// Asynchronously deletes an entity by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the entity to delete.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public virtual async Task DeleteByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			await Repository.DeleteByIdAsync(id, cancellationToken);
			await Repository.SaveChangesAsync(cancellationToken);
		}
	}
}
