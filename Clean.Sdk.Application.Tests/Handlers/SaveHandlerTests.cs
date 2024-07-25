using AutoMapper;
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
	public class SaveHandlerTests
	{
		private const string validName = "gerson";
		private const string validMiddleName = "brain";
		private const string validSurname = "sanchez";
		private const short validAge = 50;
		private const string textWith60Charters = "Lorem ipsum dolor sit amet, consectetur adipiscing elit est.";
		private const int ExpectedValidationExceptionNumber = 6;

		private readonly string ExpectedNameRequiredValidationMessage = string.Format(ValidationErrorMessages.Required, nameof(ClientTestDto.Name));
		private readonly string ExpectedNameRangeValidationMessage = string.Format(ValidationErrorMessages.Range, nameof(ClientTestDto.Name), ClientParameters.NameMinLength, ClientParameters.NameMaxLength);
		private readonly string ExpectedMiddleNameRangeValidationMessage = string.Format(ValidationErrorMessages.Range, nameof(ClientTestDto.MiddleName), ClientParameters.NameMinLength, ClientParameters.NameMaxLength);
		private readonly string ExpectedSurnameRequiredValidationMessage = string.Format(ValidationErrorMessages.Required, nameof(ClientTestDto.Surname));
		private readonly string ExpectedSurnameRangeValidationMessage = string.Format(ValidationErrorMessages.Range, nameof(ClientTestDto.Surname), ClientParameters.SurnameMinLength, ClientParameters.SurnameMaxLength);
		private readonly string ExpectedAgeMinimumAgeValidationMessage = string.Format(ValidationErrorMessages.MinimumAge, nameof(ClientTestDto.Age));

		private readonly Mock<ISaveService<ClientTest>> _mockSaveService;
		private readonly Mock<ILogger<SaveClientTestCommandHandler>> _mockLogger;
		private readonly Mock<IMapper> _mockMapper;
		private readonly SaveClientTestCommandHandler _handler;

		public SaveHandlerTests()
		{
			_mockSaveService = new Mock<ISaveService<ClientTest>>();
			_mockLogger = new Mock<ILogger<SaveClientTestCommandHandler>>();
			_mockMapper = new Mock<IMapper>();

			_handler = new SaveClientTestCommandHandler(
				_mockLogger.Object,
				_mockMapper.Object,
				new Lazy<ISaveService<ClientTest>>(() => _mockSaveService.Object));
		}

		[Fact]
		public async Task Handle_ValidRequest_ReturnsMappedResponse()
		{
			// Arrange
			var request = new SaveClientTestCommandBuilder()
				.WithName(validName)
				.WithMiddleName(validMiddleName)
				.WithSurname(validSurname)
				.WithAge(validAge)
				.Build();

			var clientTestBuilder = new ClientTestBuilder()
				.WithName(request.Name)
				.WithMiddleName(request.MiddleName)
				.WithSurname(request.Surname)
				.WithAge(request.Age);

			var clientTest = clientTestBuilder
				.BuildForCreation();

			var savedClientTest = clientTestBuilder
				.WithId(Guid.NewGuid())
				.Build();

			var resultClientTestDto = new ClientTestDtoBuilder()
				.WithId(savedClientTest.Id)
				.WithName(savedClientTest.Name)
				.WithMiddleName(savedClientTest.MiddleName)
				.WithSurname(savedClientTest.Surname)
				.WithAge(savedClientTest.Age)
				.Build();

			_mockMapper.Setup(m => m.Map<SaveClientTestCommand, ClientTest>(request))
				.Returns(clientTest);

			_mockSaveService.Setup(s => s.SaveAsync(clientTest, It.IsAny<CancellationToken>()))
				.ReturnsAsync(savedClientTest);

			_mockMapper.Setup(m => m.Map<ClientTest, ClientTestDto>(savedClientTest))
				.Returns(resultClientTestDto);

			// Act
			var result = await _handler.Handle(request, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.NotEqual(Guid.Empty, result.Id);
			Assert.Equal(request.Name, result.Name);
			Assert.Equal(request.MiddleName, result.MiddleName);
			Assert.Equal(request.Surname, result.Surname);
			Assert.Equal(request.Age, result.Age);

			_mockMapper.Verify(m => m.Map<SaveClientTestCommand, ClientTest>(It.IsAny<SaveClientTestCommand>()), Times.Once);
			_mockSaveService.Verify(x => x.SaveAsync(It.IsAny<ClientTest>(), It.IsAny<CancellationToken>()), Times.Once);
			_mockMapper.Verify(m => m.Map<ClientTest, ClientTestDto>(It.IsAny<ClientTest>()), Times.Once);
		}

		[Fact]
		public async void Handle_InvalidRequest_ThrowsValidationException()
		{
			// Arrange
			var request = new SaveClientTestCommandBuilder()
				.WithName(string.Empty) // Invalid Name
				.WithMiddleName(textWith60Charters) // Invalid MiddleName
				.WithSurname(string.Empty) // Invalid Surname
				.WithAge(0) // Invalid Age
				.Build();

			// Act
			var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(request, CancellationToken.None));

			// Assert
			Assert.NotNull(exception);
			Assert.Equal(ExpectedValidationExceptionNumber, exception.Errors.Count());
			Assert.Collection(exception.Errors,
				error => Assert.Equal(ExpectedNameRequiredValidationMessage, error.ErrorMessage),
				error => Assert.Equal(ExpectedNameRangeValidationMessage, error.ErrorMessage),
				error => Assert.Equal(ExpectedMiddleNameRangeValidationMessage, error.ErrorMessage),
				error => Assert.Equal(ExpectedSurnameRequiredValidationMessage, error.ErrorMessage),
				error => Assert.Equal(ExpectedSurnameRangeValidationMessage, error.ErrorMessage),
				error => Assert.Equal(ExpectedAgeMinimumAgeValidationMessage, error.ErrorMessage)
			);

			_mockMapper.Verify(m => m.Map<SaveClientTestCommand, ClientTest>(It.IsAny<SaveClientTestCommand>()), Times.Never);
			_mockSaveService.Verify(x => x.SaveAsync(It.IsAny<ClientTest>(), It.IsAny<CancellationToken>()), Times.Never);
			_mockMapper.Verify(m => m.Map<ClientTest, ClientTestDto>(It.IsAny<ClientTest>()), Times.Never);
		}

		[Fact]
		public async Task Handle_WithCancellationToken_CancelsOperation()
		{
			// Arrange
			var request = new SaveClientTestCommandBuilder()
				.WithName(validName)
				.WithMiddleName(validMiddleName)
				.WithSurname(validSurname)
				.WithAge(validAge)
				.Build();

			var cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.Cancel();

			_mockSaveService
				.Setup(s => s.SaveAsync(It.IsAny<ClientTest>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((ClientTest clientTest, CancellationToken cancellationToken) =>
				{
					if (cancellationToken.IsCancellationRequested)
						throw new OperationCanceledException();
					return clientTest;
				});

			// Act & Assert
			await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(request, cancellationTokenSource.Token));

			_mockSaveService.Verify(s => s.SaveAsync(It.IsAny<ClientTest>(), It.IsAny<CancellationToken>()), Times.Once);
		}
	}
}
