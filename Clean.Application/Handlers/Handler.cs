using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Clean.Application.Handlers
{
	public abstract class Handler
	{
		protected ILogger<Handler> Logger { get; set; }
		protected IMapper Mapper { get; }

		protected Handler(ILogger<Handler> logger, IMapper mapper)
		{
			Logger = logger;
			Mapper = mapper;
		}
	}
}
