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
	/// Represents an abstract class that handles the save request for a given entity.
	/// </summary>
	/// <typeparam name="TRequest">The type of the request command.</typeparam>
	/// <typeparam name="TResponse">The type of the response.</typeparam>
	/// <typeparam name="TModel">The type of the domain model.</typeparam>
	/// <typeparam name="TServie">The type of the save service.</typeparam>
	public abstract class SaveHandler<TRequest, TResponse, TModel, TServie> : CommandHandler<TServie>, IRequestHandler<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TModel : class, IDomainModel
		where TServie : class, ISaveService<TModel>
	{
		/// <summary>
		/// The logger service.
		/// </summary>
		protected readonly Lazy<ILoggerService> _logger;

		/// <summary>
		/// The mapper service.
		/// </summary>
		protected readonly IMapper _mapper;

		/// <summary>
		/// Gets the validation rules for the request.
		/// </summary>
		protected abstract AbstractValidator<TRequest>? ValidationRules { get; }


		/// <summary>
		/// Initializes a new instance of the <see cref="SaveHandler{TRequest, TResponse, TModel, TServie}"/> class.
		/// </summary>
		/// <param name="logger">The logger service.</param>
		/// <param name="mapper">The mapper service.</param>
		/// <param name="service">The save service.</param>
		protected SaveHandler(Lazy<ILoggerService> logger, IMapper mapper, Lazy<TServie> service) : base(logger, mapper, service)
		{
			this._logger = logger;
			this._mapper = mapper;
		}

		/// <summary>
		/// Handles the save request.
		/// </summary>
		/// <param name="request">The save command request.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A task representing the asynchronous operation, containing the mapped response.</returns>
		public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			Validate(request);
			var newEntity = Mapper.Map<TRequest, TModel>(request);
			var entityResult = await Service.SaveAsync(newEntity, cancellationToken);
			return Mapper.Map<TModel, TResponse>(entityResult);
		}

		/// <summary>
		/// Validates the request before handling it, if the validator is not null.
		/// </summary>
		/// <param name="request">The request to validate.</param>
		protected virtual void Validate(TRequest request)
		{
			var validation = ValidationRules?.Validate(request, options => { options.IncludeRuleSets(ValidationsSet.SAVE); options.ThrowOnFailures(); });
		}
	}
}