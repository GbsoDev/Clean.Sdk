using AutoMapper;
using Clean.Sdk.Application.Handlers;
using Clean.Sdk.Domain.Services;
using Clean.Sdk.Domain.Tests.TestEntites.Clients;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Clean.Sdk.Application.Tests.TestHandlers.Clients.Commands
{
	public sealed class RegisterClientTestCommandHandler : RegistrationHandler<RegisterClientTestCommand, ClientTestDto, ClientTest, IRegisterService<ClientTest>>
	{
		protected override AbstractValidator<RegisterClientTestCommand> ValidationRules => new RegisterClientTestCommandValidator();

		public RegisterClientTestCommandHandler(ILogger<Handler> logger, IMapper mapper, Lazy<IRegisterService<ClientTest>> service) : base(logger, mapper, service)
		{
		}
	}
}
