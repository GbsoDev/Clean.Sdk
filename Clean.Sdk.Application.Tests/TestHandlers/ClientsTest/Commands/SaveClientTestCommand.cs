using MediatR;

namespace Clean.Sdk.Application.Tests.TestHandlers.ClientsTest.Commands
{
	public record SaveClientTestCommand : ClientTestDto, IRequest<ClientTestDto>
	{
		public SaveClientTestCommand(string name, string? middleName, string surname, short age)
			: base(name, middleName, surname, age)
		{
		}
	}
}
