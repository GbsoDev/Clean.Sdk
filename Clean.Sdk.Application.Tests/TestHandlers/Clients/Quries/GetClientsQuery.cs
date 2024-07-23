using MediatR;

namespace Clean.Sdk.Application.Tests.TestHandlers.Clients.Quries
{
	public record GetClientsQuery() : IRequest<ClientTestDto[]>;
}
