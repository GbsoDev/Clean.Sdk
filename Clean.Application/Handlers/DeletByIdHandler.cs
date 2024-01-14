using AutoMapper;
using Clean.Domain.Entity;
using Clean.Domain.Services;
using Maios.CRM.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Maios.CRM.Application.Abstractions
{
	public class DeletByIdHandler<TRequest, TEntity, TServie> : CommandHandler<TServie>
		where TRequest : ICommandDeleteById
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
