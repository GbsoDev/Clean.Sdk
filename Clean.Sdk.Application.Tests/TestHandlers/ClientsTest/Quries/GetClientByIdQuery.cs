using Clean.Sdk.Application.Handlers;

namespace Clean.Sdk.Application.Tests.TestHandlers.ClientsTest.Quries
{
	public class GetClientByIdQuery : QueryById<Guid, ClientTestDto>
	{
		public GetClientByIdQuery(Guid id) : base(id)
		{
		}
	}
}
