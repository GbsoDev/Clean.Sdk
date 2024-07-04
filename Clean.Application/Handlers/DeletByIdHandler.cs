using AutoMapper;
using Clean.Domain.Entity;
using Clean.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Handlers
{
	public abstract class DeletByIdHandler<TRequest, TEntity, TServie> : CommandHandler<TServie>, IRequestHandler<TRequest>
		where TRequest : ICommandDeleteById, IRequest
		where TEntity : class, IDomainEntity
		where TServie : IDeleteService<TEntity>
	{
		public DeletByIdHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TServie> service) : base(logger, mapper, service)
		{
		}

		public async Task Handle(TRequest request, CancellationToken cancellationToken)
		{
			await Service.DeleteByIdAsync(request.Id, cancellationToken);
		}
	}
}
