using AutoMapper;
using Microsoft.Extensions.Logging;
using System;

namespace Clean.Sdk.Application.Handlers

{
	public abstract class CommandHandler<TService> : Handler
	{
		protected virtual TService Service => _service.Value;
		private readonly Lazy<TService> _service;

		protected CommandHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TService> service) : base(logger, mapper)
		{
			_service = service;
		}
	}
}
