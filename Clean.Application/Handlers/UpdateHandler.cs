using AutoMapper;
using Clean.Application.Validations;
using Clean.Domain.Entity;
using Clean.Domain.Services;
using Clean.Domain.Validations;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Maios.CRM.Application.Abstractions
{
	public abstract class UpdateHandler<TRequest, TDto, TEntity, TServie> : CommandHandler<TServie>
		where TEntity : class, IDomainEntity
		where TServie : IUpdateService<TEntity>
	{
		protected abstract AbstractValidator<TEntity> ValidationRules { get; }

		public UpdateHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TServie> service) : base(logger, mapper, service)
		{
		}

		public virtual async Task<TDto> Handle(TRequest request, CancellationToken cancellationToken)
		{
			var toUpdate = Mapper.Map<TRequest, TEntity>(request);
			Validate(toUpdate);
			var updated = await Service.UpdateAsync(toUpdate, cancellationToken);
			return Mapper.Map<TEntity, TDto>(updated);
		}

		private void Validate(TEntity newEntity)
		{
			var validation = ValidationRules.Validate(newEntity, options => options.IncludeRuleSets(ValidationsSet.CREATION));
			if (!validation.IsValid)
			{
				throw new ValidationException(string.Format(EntityValidationMessages.InvalidEntityToUpdate, typeof(TEntity).Name), validation.Errors);
			}
		}
	}
}
