using Clean.Domain.Entity;
using Clean.Domain.Ports;
using Microsoft.Extensions.Logging;
using System;

namespace Clean.Domain.Services
{
	public abstract class ActionService<TEntity, TRepository> : Service
		where TEntity : class, IDomainEntity
		where TRepository : IRepository<TEntity>
	{
		protected TRepository Repository => _repository.Value;
		private readonly Lazy<TRepository> _repository;

		protected ActionService(ILogger<Service> logger, Lazy<TRepository> repository) : base(logger)
		{
			_repository = repository;
		}
	}
}
