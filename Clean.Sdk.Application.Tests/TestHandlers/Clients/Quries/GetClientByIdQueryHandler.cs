using AutoMapper;
using Clean.Sdk.Application.Handlers;
using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Tests.TestEntites.ClientsTest;
using Microsoft.Extensions.Logging;

namespace Clean.Sdk.Application.Tests.TestHandlers.Clients.Quries
{
	public sealed class GetClientByIdQueryHandler : QueryByIdHandler<GetClientByIdQuery, ClientTestDto, ClientTest, IRepository<ClientTest>>
	{
		public GetClientByIdQueryHandler(ILogger<Handler> logger, IMapper mapper, Lazy<IRepository<ClientTest>> repository) : base(logger, mapper, repository)
		{
		}
	}
}
