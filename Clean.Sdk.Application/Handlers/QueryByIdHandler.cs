using AutoMapper;
using Clean.Sdk.Domain.Entity;
using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Ports;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Sdk.Application.Handlers
{
	public abstract class QueryByIdHandler<TRequest, TResponse, TEntity, TRepository> : QueryHandler<TRepository>, IRequestHandler<TRequest, TResponse>
		where TRequest : class, IQueryById<TResponse>, IRequest<TResponse>
		where TEntity : class, IDomainEntity
		where TRepository : class, IRepository<TEntity>
	{
		protected QueryByIdHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TRepository> repository) : base(logger, mapper, repository)
		{
		}

		public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			var id = request.Id;
			var entityResult = await Repository.ConsultByIdAsync(id, cancellationToken);
			if (entityResult == null) throw new NotFoundException(string.Format(Messages.NotFoundByIdException, typeof(TEntity).Name, id));
			return Mapper.Map<TEntity, TResponse>(entityResult);
		}
	}
}
