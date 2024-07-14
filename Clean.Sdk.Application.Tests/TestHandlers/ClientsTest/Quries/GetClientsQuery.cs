using MediatR;

namespace Clean.Sdk.Application.Tests.TestHandlers.ClientsTest.Quries
{
	public record GetClientsQuery() : IRequest<ClientTestDto[]>;
}
