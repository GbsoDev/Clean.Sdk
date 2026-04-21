using MediatR;

namespace Clean.Sdk.Application.Tests.TestHandlers.ClientsTest.Queries
{
	public record GetClientsQuery() : IRequest<ClientTestDto[]>;
}
