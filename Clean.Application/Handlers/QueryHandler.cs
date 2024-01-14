using AutoMapper;
using Clean.Domain.Ports;
using Microsoft.Extensions.Logging;
using System;

namespace Maios.CRM.Application.Abstractions
{
	public class QueryHandler<TRepository> : Handler
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
