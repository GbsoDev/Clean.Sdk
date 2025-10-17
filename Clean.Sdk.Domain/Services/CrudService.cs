using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Ports;
using Microsoft.Extensions.Logging;

namespace Clean.Sdk.Domain.Services
{
	[Obsolete("in construction", true)]
	public abstract class CrudService<TModel, TRepository> : ActionService<TModel, TRepository>, ICrudService<TModel>
		where TModel : class, IDomainModel
		where TRepository : IRepository<TModel>
	{
		protected CrudService(ILogger<Service> logger, Lazy<TRepository> repository) : base(logger, repository)
		{
		}

		public virtual async Task<TModel> SaveAsync(TModel entity, CancellationToken cancellationToken = default)
		{
			if (entity is null) throw new ArgumentNullException(nameof(entity));
			var modeloResgistrado = await Repository.SaveAsync(entity, cancellationToken);
			await Repository.SaveChangesAsync(cancellationToken);
			return modeloResgistrado;
		}

		public virtual async Task<TModel[]> LisAsync(CancellationToken cancellationToken = default)
		{
			var resultadoEntidades = await Repository.GetAllAsync(cancellationToken);
			return resultadoEntidades;
		}

		public virtual async Task<TModel?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			var resultadoEntidad = await Repository.GetByIdAsync(id, cancellationToken);
			return resultadoEntidad;
		}

		public virtual async Task<TModel> UpdateAsync(TModel entity, CancellationToken cancellationToken = default)
		{
			var resultadoEntidadActualizada = await Repository.UpdateAsync(entity, cancellationToken);
			await Repository.SaveChangesAsync(cancellationToken);
			return resultadoEntidadActualizada;
		}

		public virtual async Task DeleteByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			await Repository.DeleteByIdAsync(id, cancellationToken);
			await Repository.SaveChangesAsync(cancellationToken);
		}
	}
}
