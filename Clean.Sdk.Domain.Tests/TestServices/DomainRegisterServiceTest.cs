using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Services;
using Clean.Sdk.Domain.Tests.Builders;
using Clean.Sdk.Domain.Tests.TestEntites.ClientsTest;
using Clean.Sdk.Domain.Tests.TestServices.Clients;
using Microsoft.Extensions.Logging;
using Moq;

namespace Clean.Sdk.Domain.Tests.TestServices
{
	public class DomainRegisterServiceTest
	{
		private readonly Mock<IRepository<ClientTest>> _mockRepository;
		private readonly Mock<ILogger<Service>> _mockLogger;
		private readonly RegisterClientTestService _service;

		public DomainRegisterServiceTest()
		{
			_mockRepository = new Mock<IRepository<ClientTest>>();
			_mockLogger = new Mock<ILogger<Service>>();

			_service = new RegisterClientTestService(
				_mockLogger.Object,
				new Lazy<IRepository<ClientTest>>(() => _mockRepository.Object));
		}

		[Fact]
		public async Task RegisterAsync_ValidClient_ReturnsStoredClient()
		{
			// Arrange
			var clientTest = new ClientTestBuilder().BuildToCreate();

			_mockRepository
				.Setup(r => r.StoreAsync(clientTest, It.IsAny<CancellationToken>()))
				.ReturnsAsync(clientTest);

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _service.RegisterAsync(clientTest, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(clientTest.Id, result.Id);
			Assert.Equal(clientTest.Name, result.Name);
			Assert.Equal(clientTest.MiddleName, result.MiddleName);
			Assert.Equal(clientTest.Surname, result.Surname);
			Assert.Equal(clientTest.Age, result.Age);
			_mockRepository.Verify(r => r.StoreAsync(clientTest, It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task RegisterAsync_InvalidClient_ThrowsException()
		{
			// Arrange
			var clientTest = new ClientTestBuilder().BuildToCreate();

			_mockRepository
				.Setup(r => r.StoreAsync(clientTest, It.IsAny<CancellationToken>()))
				.ThrowsAsync(new Exception());

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _service.RegisterAsync(clientTest, CancellationToken.None));
			_mockRepository.Verify(r => r.StoreAsync(It.IsAny<ClientTest>(), It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		}

		[Fact]
		public async Task RegisterAsync_WithCancellationToken_CancelsOperation()
		{
			// Arrange
			var clientTest = new ClientTestBuilder().BuildToCreate();
			var cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.Cancel();

			_mockRepository
				.Setup(r => r.StoreAsync(clientTest, It.IsAny<CancellationToken>()))
				.ThrowsAsync(new OperationCanceledException());

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);

			// Act & Assert
			await Assert.ThrowsAsync<OperationCanceledException>(() => _service.RegisterAsync(clientTest, cancellationTokenSource.Token));
			_mockRepository.Verify(r => r.StoreAsync(It.IsAny<ClientTest>(), It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		}
	}
}
