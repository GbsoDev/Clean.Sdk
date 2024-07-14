﻿using AutoMapper;
using Clean.Sdk.Domain.Ports;
using Microsoft.Extensions.Logging;
using System;

namespace Clean.Sdk.Application.Handlers
{
	public abstract class QueryHandler<TRepository> : Handler
		where TRepository : class, IRepository
	{
		protected TRepository Repository => _repository.Value;
		private readonly Lazy<TRepository> _repository;

		public QueryHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TRepository> repository) : base(logger, mapper)
		{
			_repository = repository;
		}
	}
}
