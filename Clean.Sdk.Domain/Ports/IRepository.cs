﻿using Clean.Sdk.Domain.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Sdk.Domain.Ports
{
	public interface IRepository
	{
		Task<bool> DeleteByIdAsync(object id, CancellationToken cancellationToken = default);
		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}
	public interface IRepository<TEntity> : IRepository
		where TEntity : class, IDomainEntity
	{
		Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default);

		Task<TEntity[]> GetAllAsync(CancellationToken cancellationToken = default);

		Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);

		Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

		Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
	}
}
