using AutoMapper;
using Clean.Application;
using Clean.Application.Handlers;
using Clean.Domain.Entity;
using Clean.Domain.Exceptions;
using Clean.Domain.Ports;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Maios.CRM.Application.Abstractions
{
	public abstract class QueryByIdHandler<TRequest, TDto, TModel, TRepository> : QueryHandler<TRepository>
		where TRequest : class, IQueryById
		where TModel : class, IDomainEntity
		where TRepository : class, IRepository<TModel>
	{
		protected QueryByIdHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TRepository> repository) : base(logger, mapper, repository)
		{
		}

		public virtual async Task<TDto> Handle(TRequest request, CancellationToken cancellationToken)
		{
			var id = request.Id;
			var entityResult = await Repository.ConsultByIdAsync(id, cancellationToken);
			if (entityResult == null) throw new NotFoundException(string.Format(Messages.NotFoundByIdException, typeof(TModel).Name, id));
			return Mapper.Map<TModel, TDto>(entityResult);
		}
	}
}
