using AutoMapper;
using Clean.Sdk.Application.Handlers;
using Clean.Sdk.Application.Tests.TestHandlers.ClientsTest.Commands;
using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Resources;
using Clean.Sdk.Domain.Services;
using Clean.Sdk.Domain.Tests.TestEntites.ClientsTest;
using Microsoft.Extensions.Logging;
using Moq;

namespace Clean.Sdk.Application.Tests.Handlers
{
	public class DeleteHandlerTests
	{
		private readonly Mock<IDeleteService<ClientTest>> _mockDeleteService;
		private readonly Mock<ILogger<Handler>> _mockLogger;
		private readonly Mock<IMapper> _mockMapper;
		private readonly DeleteClientTestByIdCommandHandler<IDeleteService<ClientTest>> _handler;

		public DeleteHandlerTests()
		{
			_mockDeleteService = new Mock<IDeleteService<ClientTest>>();
			_mockLogger = new Mock<ILogger<Handler>>();
			_mockMapper = new Mock<IMapper>();

			_handler = new DeleteClientTestByIdCommandHandler<IDeleteService<ClientTest>>(
				_mockLogger.Object,
				_mockMapper.Object,
				new Lazy<IDeleteService<ClientTest>>(() => _mockDeleteService.Object));
		}

		[Fact]
		public async Task Handle_ValidRequest_DeletesEntity()
		{
			// Arrange
			var request = new DeleteClientTestByIdCommand(Guid.NewGuid());

			_mockDeleteService.Setup(s => s.DeleteByIdAsync(request.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(true);

			// Act
			await _handler.Handle(request, CancellationToken.None);

			// Assert
			_mockDeleteService.Verify(s => s.DeleteByIdAsync(request.Id, It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task Handle_EntityNotFound_ThrowsNotFoundException()
		{
			// Arrange
			var request = new DeleteClientTestByIdCommand(Guid.NewGuid());

			_mockDeleteService.Setup(s => s.DeleteByIdAsync(request.Id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(false);

			// Act & Assert
			var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));

			Assert.Equal(string.Format(Messages.NotFoundByIdException, typeof(ClientTest).Name, request.Id), exception.Message);
			_mockDeleteService.Verify(s => s.DeleteByIdAsync(request.Id, It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task Handle_WithCancellationToken_CancelsOperation()
		{
			// Arrange
			var request = new DeleteClientTestByIdCommand(Guid.NewGuid());
			var cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.Cancel();

			_mockDeleteService
				.Setup(s => s.DeleteByIdAsync(It.IsAny<object>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((object id, CancellationToken cancellationToken) =>
				{
					if (cancellationToken.IsCancellationRequested)
						throw new OperationCanceledException();
					return true;
				});

			// Act & Assert
			await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(request, cancellationTokenSource.Token));

			_mockDeleteService.Verify(s => s.DeleteByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
		}
	}
}
