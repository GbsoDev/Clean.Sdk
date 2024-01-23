using AutoMapper;
using Clean.Domain.Entity;
using Clean.Domain.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Handlers
{
	public class QueryCollectionHandler<TRequest, TResponse, TEntity, TRepository> : QueryHandler<TRepository>, IRequestHandler<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TResponse : ICollection
		where TEntity : class, IDomainEntity
		where TRepository : class, IRepository<TEntity>
	{
		public QueryCollectionHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TRepository> repository) : base(logger, mapper, repository)
		{
		}

		public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			var entityResult = await Repository.ConsultAllAsync(cancellationToken);
			return Mapper.Map<TEntity[], TResponse>(entityResult);
		}
	}
}
