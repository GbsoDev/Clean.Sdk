using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Services;
using Clean.Sdk.Domain.Tests.Builders;
using Clean.Sdk.Domain.Tests.TestEntites.ClientsTest;
using Clean.Sdk.Domain.Tests.TestServices.Clients;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;

namespace Clean.Sdk.Domain.Tests.TestServices
{
	public class DomainSaveServiceTest
	{
		private readonly Mock<IRepository<ClientTest>> _mockRepository;
		private readonly Mock<ILogger<Service>> _mockLogger;
		private readonly SaveClientTestService _service;

		public DomainSaveServiceTest()
		{
			_mockRepository = new Mock<IRepository<ClientTest>>();
			_mockLogger = new Mock<ILogger<Service>>();

			_service = new SaveClientTestService(
				_mockLogger.Object,
				new Lazy<IRepository<ClientTest>>(() => _mockRepository.Object));
		}

		[Fact]
		public async Task SaveAsync_ValidClient_ReturnsStoredClient()
		{
			// Arrange
			var clientTest = new ClientTestBuilder().BuildToCreate();

			_mockRepository
				.Setup(r => r.SaveAsync(clientTest, It.IsAny<CancellationToken>()))
				.ReturnsAsync(clientTest);

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _service.SaveAsync(clientTest, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(clientTest.Id, result.Id);
			Assert.Equal(clientTest.Name, result.Name);
			Assert.Equal(clientTest.MiddleName, result.MiddleName);
			Assert.Equal(clientTest.Surname, result.Surname);
			Assert.Equal(clientTest.Age, result.Age);
			_mockRepository.Verify(r => r.SaveAsync(clientTest, It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task SaveAsync_InvalidClient_ThrowsException()
		{
			// Arrange
			var clientTest = new ClientTestBuilder().BuildToCreate();

			_mockRepository
				.Setup(r => r.SaveAsync(clientTest, It.IsAny<CancellationToken>()))
				.ThrowsAsync(new Exception());

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _service.SaveAsync(clientTest, CancellationToken.None));
			_mockRepository.Verify(r => r.SaveAsync(It.IsAny<ClientTest>(), It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		}

		[Fact]
		public async Task SaveAsync_WithCancellationToken_CancelsOperation()
		{
			// Arrange
			var clientTest = new ClientTestBuilder().BuildToCreate();
			var cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.Cancel();

			_mockRepository
				.Setup(r => r.SaveAsync(clientTest, It.IsAny<CancellationToken>()))
				.ReturnsAsync((ClientTest clentTest, CancellationToken cancellationToken) =>
				{
					if (cancellationToken.IsCancellationRequested)
						throw new OperationCanceledException();
					return clientTest;
				});

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Callback((CancellationToken cancellationToken) =>
				{
					if (cancellationToken.IsCancellationRequested)
						throw new OperationCanceledException();
				});

			// Act & Assert
			await Assert.ThrowsAsync<OperationCanceledException>(() => _service.SaveAsync(clientTest, cancellationTokenSource.Token));
			_mockRepository.Verify(r => r.SaveAsync(It.IsAny<ClientTest>(), It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		}
	}
}
