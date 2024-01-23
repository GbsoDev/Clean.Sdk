using AutoMapper;
using Clean.Application.Validations;
using Clean.Domain.Entity;
using Clean.Domain.Services;
using Clean.Domain.Validations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Application.Handlers
{
	public abstract class UpdateHandler<TRequest, TResponse, TEntity, TServie> : CommandHandler<TServie>, IRequestHandler<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TEntity : class, IDomainEntity
		where TServie : IUpdateService<TEntity>
	{
		protected abstract AbstractValidator<TRequest> ValidationRules { get; }

		public UpdateHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TServie> service) : base(logger, mapper, service)
		{
		}

		public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			Validate(request);
			var toUpdate = Mapper.Map<TRequest, TEntity>(request);
			var updated = await Service.UpdateAsync(toUpdate, cancellationToken);
			return Mapper.Map<TEntity, TResponse>(updated);
		}

		private void Validate(TRequest request)
		{
			var validation = ValidationRules.Validate(request, options => options.IncludeRuleSets(ValidationsSet.CREATION));
			if (!validation.IsValid)
			{
				throw new ValidationException(string.Format(EntityValidationMessages.InvalidEntityToUpdate, typeof(TEntity).Name), validation.Errors);
			}
		}
	}
}
