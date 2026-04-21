using Clean.Sdk.Application.Handlers;

namespace Clean.Sdk.Application.Tests.TestHandlers.ClientsTest.Queries
{
	public class GetClientByIdQuery : QueryById<Guid, ClientTestDto>
	{
		public GetClientByIdQuery(Guid id) : base(id)
		{
		}
	}
}
