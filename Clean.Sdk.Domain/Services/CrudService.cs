﻿using Clean.Sdk.Domain.Entity;
using Clean.Sdk.Domain.Ports;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Sdk.Domain.Services
{
	[Obsolete("in construction", true)]
	public abstract class CrudService<TEntity, TRepository> : ActionService<TEntity, TRepository>, ICrudService<TEntity>
		where TEntity : class, IDomainEntity
		where TRepository : IRepository<TEntity>
	{
		protected CrudService(ILogger<Service> logger, Lazy<TRepository> repository) : base(logger, repository)
		{
		}

		public virtual async Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default)
		{
			if (entity is null) throw new ArgumentNullException(nameof(entity));
			var modeloResgistrado = await Repository.SaveAsync(entity, cancellationToken);
			await Repository.SaveChangesAsync(cancellationToken);
			return modeloResgistrado;
		}

		public virtual async Task<TEntity[]> LisAsync(CancellationToken cancellationToken = default)
		{
			var resultadoEntidades = await Repository.GetAllAsync(cancellationToken);
			return resultadoEntidades;
		}

		public virtual async Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
		{
			var resultadoEntidad = await Repository.GetByIdAsync(id, cancellationToken);
			return resultadoEntidad;
		}

		public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
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
