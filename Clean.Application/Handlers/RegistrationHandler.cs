using AutoMapper;
using Clean.Application.Validations;
using Clean.Domain.Entity;
using Clean.Domain.Entity.Validations;
using Clean.Domain.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Maios.CRM.Application.Abstractions
{
    public abstract class RegistrationHandler<TRequest, TDto, TEntity, TServie> : CommandHandler<TServie>
		where TEntity : class, IDomainEntity
		where TServie : IRegisterService<TEntity>
	{
		protected abstract AbstractValidator<TEntity> ValidationRules { get; }

		protected RegistrationHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TServie> service) : base(logger, mapper, service)
		{
		}

		public virtual async Task<TDto> Handle(TRequest request, CancellationToken cancellationToken)
		{
			var newEntity = Mapper.Map<TRequest, TEntity>(request);
			Validate(newEntity);
			var entityResult = await Service.RegisterAsync(newEntity, cancellationToken);
			return Mapper.Map<TEntity, TDto>(entityResult);
		}

		private void Validate(TEntity newEntity)
		{
			var validation = ValidationRules.Validate(newEntity, options => options.IncludeRuleSets(ValidationsSet.CREATION));
			if (!validation.IsValid)
			{
				throw new ValidationException(string.Format(EntityValidationMessages.InvalidEntityToCreate, typeof(TEntity).Name), validation.Errors);
			}
		}
	}
}
