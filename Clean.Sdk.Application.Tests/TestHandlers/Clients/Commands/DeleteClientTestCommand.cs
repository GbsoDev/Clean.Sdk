using Clean.Sdk.Application.Handlers;

namespace Clean.Sdk.Application.Tests.TestHandlers.Clients.Commands
{
	public class DeleteClientTestCommand : CommandDeleteById<Guid>
	{
		public DeleteClientTestCommand(Guid id) : base(id)
		{
		}
	}
}
