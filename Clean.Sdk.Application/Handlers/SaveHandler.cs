﻿using AutoMapper;
using Clean.Sdk.Application.Validations;
using Clean.Sdk.Domain.Entity;
using Clean.Sdk.Domain.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Sdk.Application.Handlers
{
	/// <summary>
	/// Represents an abstract class that handles the save request for a given entity.
	/// </summary>
	public abstract class SaveHandler<TRequest, TResponse, TEntity, TServie> : CommandHandler<TServie>, IRequestHandler<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TEntity : class, IDomainEntity
		where TServie : class, ISaveService<TEntity>
	{
		protected readonly ILogger<Handler> _logger;
		protected readonly IMapper _mapper;

		protected abstract AbstractValidator<TRequest>? ValidationRules { get; }


		/// <summary>
		/// Initializes a new instance of the <see cref="SaveHandler{TRequest, TResponse, TEntity, TServie}"/> class.
		/// </summary>
		protected SaveHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TServie> service) : base(logger, mapper, service)
		{
			this._logger = logger;
			this._mapper = mapper;
		}

		/// <summary>
		/// Handles the save request.
		/// </summary>
		public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			Validate(request);
			var newEntity = Mapper.Map<TRequest, TEntity>(request);
			var entityResult = await Service.SaveAsync(newEntity, cancellationToken);
			return Mapper.Map<TEntity, TResponse>(entityResult);
		}

		/// <summary>
		/// Validates the request before handle, if the validator is not null.
		/// </summary>
		protected virtual void Validate(TRequest request)
		{
			var validation = ValidationRules?.Validate(request, options => { options.IncludeRuleSets(ValidationsSet.SAVE); options.ThrowOnFailures(); });
		}
	}
}