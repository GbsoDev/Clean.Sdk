using AutoMapper;
using Clean.Sdk.Application.Handlers;
using Clean.Sdk.Domain.Services;
using Clean.Sdk.Domain.Tests.TestModel.ClientsTest;
using FluentValidation;
using MediatR;
using Clean.Sdk.Domain.Ports;

namespace Clean.Sdk.Application.Tests.TestHandlers.ClientsTest.Commands
{
	public sealed class UpdateClientTestCommandHandler : UpdateHandler<UpdateClientTestCommand, ClientTestDto, ClientTest, IUpdateService<ClientTest>>, IRequestHandler<UpdateClientTestCommand, ClientTestDto>
	{
		protected override AbstractValidator<UpdateClientTestCommand> ValidationRules => new UpdateClientTestCommandValidator();

		public UpdateClientTestCommandHandler(Lazy<ILoggerService> logger, IMapper mapper, Lazy<IUpdateService<ClientTest>> service) : base(logger, mapper, service)
		{
		}
	}
}
