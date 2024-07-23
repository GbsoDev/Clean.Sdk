using Clean.Sdk.Application.Handlers;

namespace Clean.Sdk.Application.Tests.TestHandlers.Clients.Quries
{
	public class GetClientByIdQuery : QueryById<Guid, ClientTestDto>
	{
		public GetClientByIdQuery(Guid id) : base(id)
		{
		}
	}
}
