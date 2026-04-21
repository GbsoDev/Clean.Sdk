using AutoMapper;
using Clean.Sdk.Application.Validations;
using Clean.Sdk.Domain.Model;
using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Services;
using FluentValidation;
using MediatR;

namespace Clean.Sdk.Application.Handlers
{
	/// <summary>
	/// Represents an abstract class that handles the update request for a given entity.
	/// </summary>
	/// <typeparam name="TRequest">The type of the request command.</typeparam>
	/// <typeparam name="TResponse">The type of the response.</typeparam>
	/// <typeparam name="TModel">The type of the domain model.</typeparam>
	/// <typeparam name="TServie">The type of the update service.</typeparam>
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
		/// Initializes a new instance of the <see cref="UpdateHandler{TRequest, TResponse, TModel, TServie}"/> class.
		/// </summary>
		/// <param name="logger">The logger service.</param>
		/// <param name="mapper">The mapper service.</param>
		/// <param name="service">The update service.</param>
		protected UpdateHandler(Lazy<ILoggerService> logger, IMapper mapper, Lazy<TServie> service) : base(logger, mapper, service)
		{
		}

		/// <summary>
		/// Handles the update request.
		/// </summary>
		/// <param name="request">The update command request.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A task representing the asynchronous operation, containing the mapped response.</returns>
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
		/// <param name="request">The request to validate.</param>
		protected virtual void Validate(TRequest request)
		{
			var validation = ValidationRules?.Validate(request, options => { options.IncludeRuleSets(ValidationsSet.UPDATE); options.ThrowOnFailures(); });
		}
	}
}