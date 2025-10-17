using AutoMapper;
using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections;

namespace Clean.Sdk.Application.Handlers
{
	public abstract class QueryCollectionHandler<TRequest, TResponse, TModel, TRepository> : QueryHandler<TRepository>, IRequestHandler<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TResponse : ICollection
		where TModel : class, IDomainModel
		where TRepository : class, IRepository<TModel>
	{
		public QueryCollectionHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TRepository> repository) : base(logger, mapper, repository)
		{
		}

		public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			var entityResult = await Repository.GetAllAsync(cancellationToken);
			return Mapper.Map<TModel[], TResponse>(entityResult);
		}
	}
}
