using AutoMapper;
using Clean.Sdk.Application.Validations;
using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean.Sdk.Application.Handlers
{
	/// <summary>
	/// Represents an abstract class that handles the update request for a given entity.
	/// </summary>
	public abstract class UpdateHandler<TRequest, TResponse, TModel, TServie> : CommandHandler<TServie>, IRequestHandler<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TModel : class, IDomainModel
		where TServie : class, IUpdateService<TModel>
	{
		/// <summary>
		/// Gets the validation rules for the request.
		/// </summary>
		protected abstract AbstractValidator<TRequest>? ValidationRules { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="UpdateHandler{TRequest, TResponse, TEntity, TServie}"/> class.
		/// </summary>
		protected UpdateHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TServie> service) : base(logger, mapper, service)
		{
		}

		/// <summary>
		/// Handles the update request.
		/// </summary>
		public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			Validate(request);
			var toUpdate = Mapper.Map<TRequest, TModel>(request);
			var updated = await Service.UpdateAsync(toUpdate, cancellationToken);
			return Mapper.Map<TModel, TResponse>(updated);
		}

		/// <summary>
		/// Validates the request before update, if the validator is not null.
		/// </summary>
		protected virtual void Validate(TRequest request)
		{
			var validation = ValidationRules?.Validate(request, options => { options.IncludeRuleSets(ValidationsSet.UPDATE); options.ThrowOnFailures(); });
		}
	}
}