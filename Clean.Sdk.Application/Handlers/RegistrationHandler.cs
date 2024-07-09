using AutoMapper;
using Clean.Application.Validations;
using Clean.Domain.Entity;
using Clean.Domain.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Handlers
{
	public abstract class RegistrationHandler<TRequest, TResponse, TEntity, TServie> : CommandHandler<TServie>, IRequestHandler<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TEntity : class, IDomainEntity
		where TServie : IRegisterService<TEntity>
	{
		protected abstract AbstractValidator<TRequest> ValidationRules { get; }

		protected RegistrationHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TServie> service) : base(logger, mapper, service)
		{
		}

		public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			Validate(request);
			var newEntity = Mapper.Map<TRequest, TEntity>(request);
			var entityResult = await Service.RegisterAsync(newEntity, cancellationToken);
			return Mapper.Map<TEntity, TResponse>(entityResult);
		}

		protected virtual void Validate(TRequest request)
		{
			var validation = ValidationRules.Validate(request, options => { options.IncludeRuleSets(ValidationsSet.UPDATE); options.ThrowOnFailures(); });
		}
	}
}
