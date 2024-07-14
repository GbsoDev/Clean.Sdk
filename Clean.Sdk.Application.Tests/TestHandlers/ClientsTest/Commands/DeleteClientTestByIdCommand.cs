using Clean.Sdk.Application.Handlers;

namespace Clean.Sdk.Application.Tests.TestHandlers.ClientsTest.Commands
{
	public class DeleteClientTestByIdCommand : CommandDeleteById<Guid>
	{
		public DeleteClientTestByIdCommand(Guid id) : base(id)
		{
		}
	}
}
