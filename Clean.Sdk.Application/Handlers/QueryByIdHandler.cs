using AutoMapper;
using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Resources;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean.Sdk.Application.Handlers
{
	public abstract class QueryByIdHandler<TRequest, TResponse, TModel, TRepository> : QueryHandler<TRepository>, IRequestHandler<TRequest, TResponse>
		where TRequest : class, IQueryById<TResponse>, IRequest<TResponse>
		where TModel : class, IDomainModel
		where TRepository : class, IRepository<TModel>
	{
		protected QueryByIdHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TRepository> repository) : base(logger, mapper, repository)
		{
		}

		public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			var id = request.Id;
			var entityResult = await Repository.GetByIdAsync(id, cancellationToken);
			if (entityResult == null) throw new NotFoundException(string.Format(Messages.NotFoundByIdException, typeof(TModel).Name, id));
			return Mapper.Map<TModel, TResponse>(entityResult);
		}
	}
}
