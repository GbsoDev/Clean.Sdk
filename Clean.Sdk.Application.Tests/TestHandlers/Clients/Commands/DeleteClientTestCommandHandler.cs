using AutoMapper;
using Clean.Sdk.Application.Handlers;
using Clean.Sdk.Domain.Services;
using Clean.Sdk.Domain.Tests.TestEntites.Clients;
using Microsoft.Extensions.Logging;

namespace Clean.Sdk.Application.Tests.TestHandlers.Clients.Commands
{
	public sealed class DeleteClientTestCommandHandler<TDeleteClientTestService> : DeletByIdHandler<DeleteClientTestCommand, ClientTest, TDeleteClientTestService>
		where TDeleteClientTestService : class, IDeleteService<ClientTest>
	{
		public DeleteClientTestCommandHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TDeleteClientTestService> service) : base(logger, mapper, service)
		{
		}
	}
}
