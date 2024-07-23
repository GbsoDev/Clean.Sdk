using AutoMapper;
using Clean.Sdk.Domain.Entity;
using Clean.Sdk.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Sdk.Application.Handlers
{
	public abstract class DeletByIdHandler<TRequest, TEntity, TServie> : CommandHandler<TServie>, IRequestHandler<TRequest>
		where TRequest : ICommandDeleteById, IRequest
		where TEntity : class, IDomainEntity
		where TServie : class, IDeleteService<TEntity>
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
