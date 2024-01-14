using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Maios.CRM.Application.Abstractions
{
	public abstract class Handler
	{
		public ILogger<Handler> Logger { get; set; }
		public IMapper Mapper { get; }

		protected Handler(ILogger<Handler> logger, IMapper mapper)
		{
			Logger = logger;
			Mapper = mapper;
		}
	}
}
