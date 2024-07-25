﻿using AutoMapper;
using Clean.Sdk.Application.Tests.DataBuilders;
using Clean.Sdk.Application.Tests.TestHandlers.ClientsTest;
using Clean.Sdk.Application.Tests.TestHandlers.ClientsTest.Commands;
using Clean.Sdk.Domain.Services;
using Clean.Sdk.Domain.Tests.TestEntites;
using Clean.Sdk.Domain.Tests.TestEntites.Clients;
using Clean.Sdk.Domain.Tests.TestEntites.ClientsTest;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;

namespace Clean.Sdk.Application.Tests.Handlers
{
	public class UpdateHandlerTests
	{
		private const string validName = "gerson";
		private const string validMiddleName = "brain";
		private const string validSurname = "sanchez";
		private const short validAge = 50;
		private const string textWith60Charters = "Lorem ipsum dolor sit amet, consectetur adipiscing elit est.";
		private const int ExpectedValidationExceptionNumber = 7;

		private readonly string ExpectedIdRequiredValidationMessage = string.Format(ValidationErrorMessages.Required, nameof(ClientTestDto.Id));
		private readonly string ExpectedNameRequiredValidationMessage = string.Format(ValidationErrorMessages.Required, nameof(ClientTestDto.Name));
		private readonly string ExpectedNameRangeValidationMessage = string.Format(ValidationErrorMessages.Range, nameof(ClientTestDto.Name), ClientParameters.NameMinLength, ClientParameters.NameMaxLength);
		private readonly string ExpectedMiddleNameRangeValidationMessage = string.Format(ValidationErrorMessages.Range, nameof(ClientTestDto.MiddleName), ClientParameters.NameMinLength, ClientParameters.NameMaxLength);
		private readonly string ExpectedSurnameRequiredValidationMessage = string.Format(ValidationErrorMessages.Required, nameof(ClientTestDto.Surname));
		private readonly string ExpectedSurnameRangeValidationMessage = string.Format(ValidationErrorMessages.Range, nameof(ClientTestDto.Surname), ClientParameters.SurnameMinLength, ClientParameters.SurnameMaxLength);
		private readonly string ExpectedAgeMinimumAgeValidationMessage = string.Format(ValidationErrorMessages.MinimumAge, nameof(ClientTestDto.Age));

		private readonly Mock<IUpdateService<ClientTest>> _mockUpdateService;
		private readonly Mock<ILogger<UpdateClientTestCommandHandler>> _mockLogger;
		private readonly Mock<IMapper> _mockMapper;
		private readonly UpdateClientTestCommandHandler _handler;

		public UpdateHandlerTests()
		{
			_mockUpdateService = new Mock<IUpdateService<ClientTest>>();
			_mockLogger = new Mock<ILogger<UpdateClientTestCommandHandler>>();
			_mockMapper = new Mock<IMapper>();

			_handler = new UpdateClientTestCommandHandler(
				_mockLogger.Object,
				_mockMapper.Object,
				new Lazy<IUpdateService<ClientTest>>(() => _mockUpdateService.Object));
		}

		[Fact]
		public async Task Handle_ValidRequest_ReturnsMappedResponse()
		{
			// Arrange
			var request = new UpdateClientTestCommandBuilder()
				.WithId(Guid.NewGuid())
				.WithName(validName)
				.WithMiddleName(validMiddleName)
				.WithSurname(validSurname)
				.WithAge(validAge)
				.Build();

			var clientTestBuilder = new ClientTestBuilder()
				.WithId(request.Id)
				.WithName(request.Name)
				.WithMiddleName(request.MiddleName)
				.WithSurname(request.Surname)
				.WithAge(request.Age);

			var clientTest = clientTestBuilder
				.Build();

			var updatedClientTest = clientTestBuilder
				.Build();

			var updatedClientTestDto = new ClientTestDtoBuilder()
				.WithId(request.Id)
				.WithName(request.Name)
				.WithMiddleName(request.MiddleName)
				.WithSurname(request.Surname)
				.WithAge(request.Age)
				.Build();

			_mockMapper.Setup(m => m.Map<UpdateClientTestCommand, ClientTest>(request))
				.Returns(clientTest);

			_mockUpdateService.Setup(s => s.UpdateAsync(clientTest, It.IsAny<CancellationToken>()))
				.ReturnsAsync(updatedClientTest);

			_mockMapper.Setup(m => m.Map<ClientTest, ClientTestDto>(updatedClientTest))
				.Returns(updatedClientTestDto);

			// Act
			var result = await _handler.Handle(request, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(request.Id, result.Id);
			Assert.Equal(request.Name, result.Name);
			Assert.Equal(request.MiddleName, result.MiddleName);
			Assert.Equal(request.Surname, result.Surname);
			Assert.Equal(request.Age, result.Age);

			_mockMapper.Verify(m => m.Map<UpdateClientTestCommand, ClientTest>(It.IsAny<UpdateClientTestCommand>()), Times.Once);
			_mockUpdateService.Verify(x => x.UpdateAsync(It.IsAny<ClientTest>(), It.IsAny<CancellationToken>()), Times.Once);
			_mockMapper.Verify(m => m.Map<ClientTest, ClientTestDto>(It.IsAny<ClientTest>()), Times.Once);
		}

		[Fact]
		public async void Handle_InvalidRequest_ThrowsValidationException()
		{
			// Arrange
			var request = new UpdateClientTestCommandBuilder()
				.WithId(Guid.Empty) // Invalid ID	
				.WithName(string.Empty) // Invalid Name
				.WithMiddleName(textWith60Charters) // Invalid Surname
				.WithSurname(string.Empty) // Invalid Surname
				.WithAge(0) // Invalid Age
				.Build();

			// Act
			var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(request, CancellationToken.None));

			// Assert
			Assert.NotNull(exception);
			Assert.Equal(ExpectedValidationExceptionNumber, exception.Errors.Count());
			Assert.Collection(exception.Errors,
				error => Assert.Equal(ExpectedIdRequiredValidationMessage, error.ErrorMessage),
				error => Assert.Equal(ExpectedNameRequiredValidationMessage, error.ErrorMessage),
				error => Assert.Equal(ExpectedNameRangeValidationMessage, error.ErrorMessage),
				error => Assert.Equal(ExpectedMiddleNameRangeValidationMessage, error.ErrorMessage),
				error => Assert.Equal(ExpectedSurnameRequiredValidationMessage, error.ErrorMessage),
				error => Assert.Equal(ExpectedSurnameRangeValidationMessage, error.ErrorMessage),
				error => Assert.Equal(ExpectedAgeMinimumAgeValidationMessage, error.ErrorMessage)
			);

			_mockMapper.Verify(m => m.Map<UpdateClientTestCommand, ClientTest>(It.IsAny<UpdateClientTestCommand>()), Times.Never);
			_mockUpdateService.Verify(x => x.UpdateAsync(It.IsAny<ClientTest>(), It.IsAny<CancellationToken>()), Times.Never);
			_mockMapper.Verify(m => m.Map<ClientTest, ClientTestDto>(It.IsAny<ClientTest>()), Times.Never);
		}

		[Fact]
		public async Task Handle_WithCancellationToken_CancelsOperation()
		{
			// Arrange
			var request = new UpdateClientTestCommandBuilder()
				.WithId(Guid.NewGuid())
				.WithName(validName)
				.WithMiddleName(validMiddleName)
				.WithSurname(validSurname)
				.WithAge(validAge)
				.Build();

			var cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.Cancel();

			_mockUpdateService
				.Setup(s => s.UpdateAsync(It.IsAny<ClientTest>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((ClientTest clientTest, CancellationToken cancellationToken) =>
				{
					if (cancellationToken.IsCancellationRequested)
						throw new OperationCanceledException();
					return clientTest;
				});

			// Act & Assert
			await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(request, cancellationTokenSource.Token));

			_mockUpdateService.Verify(s => s.UpdateAsync(It.IsAny<ClientTest>(), It.IsAny<CancellationToken>()), Times.Once);
		}
	}
}