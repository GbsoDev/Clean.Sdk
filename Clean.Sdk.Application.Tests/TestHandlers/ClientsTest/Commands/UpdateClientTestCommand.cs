using MediatR;

namespace Clean.Sdk.Application.Tests.TestHandlers.ClientsTest.Commands
{
	public record UpdateClientTestCommand : ClientTestDto, IRequest<ClientTestDto>
	{
		public UpdateClientTestCommand(Guid id, string name, string? middleName, string surname, short age)
			: base(id, name, middleName, surname, age)
		{
		}
	}
}
