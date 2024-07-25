using AutoMapper;
using Clean.Sdk.Application.Handlers;
using Clean.Sdk.Domain.Services;
using Clean.Sdk.Domain.Tests.TestEntites.ClientsTest;
using Microsoft.Extensions.Logging;

namespace Clean.Sdk.Application.Tests.TestHandlers.ClientsTest.Commands
{
	public sealed class DeleteClientTestByIdCommandHandler<TDeleteClientTestService> : CommandDeleteByIdHandler<DeleteClientTestByIdCommand, ClientTest, TDeleteClientTestService>
		where TDeleteClientTestService : class, IDeleteService<ClientTest>
	{
		public DeleteClientTestByIdCommandHandler(ILogger<Handler> logger, IMapper mapper, Lazy<TDeleteClientTestService> service) : base(logger, mapper, service)
		{
		}
	}
}
