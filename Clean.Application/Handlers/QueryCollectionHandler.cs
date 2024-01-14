using AutoMapper;
using Clean.Domain.Entity;
using Clean.Domain.Ports;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace Maios.CRM.Application.Abstractions
{
	public class QueryCollectionHandler<TRequest, TDto, TEntity, TRepository> : QueryHandler<TRepository>
		where TDto : ICollection
		where TEntity : class, IDomainEntity
		where TRepository : class, IRepository<TEntity>
	{
		public QueryCollectionHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TRepository> repository) : base(logger, mapper, repository)
		{
		}

		public virtual async Task<TDto> Handle(TRequest request, CancellationToken cancellationToken)
		{
			var entityResult = await Repository.ConsultAllAsync(cancellationToken);
			return Mapper.Map<TEntity[], TDto>(entityResult);
		}
	}
}
