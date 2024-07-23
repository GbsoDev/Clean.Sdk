using MediatR;

namespace Clean.Sdk.Application.Tests.TestHandlers.Clients.Commands
{
	public record RegisterClientTestCommand : ClientTestDto, IRequest<ClientTestDto>
	{
		public RegisterClientTestCommand(string name, string? middleName, string surname, short age)
			: base(name, middleName, surname, age)
		{
		}
	}
}
