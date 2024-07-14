using AutoMapper;
using Clean.Sdk.Application.Handlers;
using Clean.Sdk.Domain.Services;
using Clean.Sdk.Domain.Tests.TestEntites.ClientsTest;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Clean.Sdk.Application.Tests.TestHandlers.ClientsTest.Commands
{
	public sealed class SaveClientTestCommandHandler : SaveHandler<SaveClientTestCommand, ClientTestDto, ClientTest, ISaveService<ClientTest>>
	{
		protected override AbstractValidator<SaveClientTestCommand> ValidationRules => new SaveClientTestCommandValidator();

		public SaveClientTestCommandHandler(ILogger<Handler> logger, IMapper mapper, Lazy<ISaveService<ClientTest>> service) : base(logger, mapper, service)
		{
		}
	}
}
