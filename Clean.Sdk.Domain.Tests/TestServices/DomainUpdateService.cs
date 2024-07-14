using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Services;
using Clean.Sdk.Domain.Tests.Builders;
using Clean.Sdk.Domain.Tests.TestEntites.ClientsTest;
using Clean.Sdk.Domain.Tests.TestServices.ClientsTest;
using Microsoft.Extensions.Logging;
using Moq;

namespace Clean.Sdk.Domain.Tests.TestServices
{
	public class DomainUpdateService
	{
		private readonly Mock<IRepository<ClientTest>> _mockRepository;
		private readonly Mock<ILogger<Service>> _mockLogger;
		private readonly UpdateClientTestService _service;

		public DomainUpdateService()
		{
			_mockRepository = new Mock<IRepository<ClientTest>>();
			_mockLogger = new Mock<ILogger<Service>>();

			_service = new UpdateClientTestService(
				_mockLogger.Object,
				new Lazy<IRepository<ClientTest>>(() => _mockRepository.Object));
		}

		[Fact]
		public async Task UpdateAsync_ValidClient_ReturnsUpdatedClient()
		{
			// Arrange
			var clientTest = new ClientTestBuilder().Build();

			_mockRepository
				.Setup(r => r.UpdateAsync(clientTest, It.IsAny<CancellationToken>()))
				.ReturnsAsync(clientTest);

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _service.UpdateAsync(clientTest, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(clientTest.Id, result.Id);
			Assert.Equal(clientTest.Name, result.Name);
			Assert.Equal(clientTest.MiddleName, result.MiddleName);
			Assert.Equal(clientTest.Surname, result.Surname);
			Assert.Equal(clientTest.Age, result.Age);
			_mockRepository.Verify(r => r.UpdateAsync(clientTest, It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task UpdateAsync_InvalidClient_ThrowsException()
		{
			// Arrange
			var clientTest = new ClientTestBuilder().Build();

			_mockRepository
				.Setup(r => r.UpdateAsync(clientTest, It.IsAny<CancellationToken>()))
				.ThrowsAsync(new Exception());

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);

			// Act & Assert
			await Assert.ThrowsAsync<Exception>(() => _service.UpdateAsync(clientTest, CancellationToken.None));
			_mockRepository.Verify(r => r.UpdateAsync(It.IsAny<ClientTest>(), It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		}

		[Fact]
		public async Task UpdateAsync_WithCancellationToken_CancelsOperation()
		{
			// Arrange
			var clientTest = new ClientTestBuilder().Build();
			var cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.Cancel();

			_mockRepository
				.Setup(r => r.UpdateAsync(clientTest, It.IsAny<CancellationToken>()))
				.ReturnsAsync((ClientTest clentTest, CancellationToken cancellationToken) =>
				{
					if (cancellationToken.IsCancellationRequested)
						throw new OperationCanceledException();
					return clientTest;
				});

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);

			// Act & Assert
			await Assert.ThrowsAsync<OperationCanceledException>(() => _service.UpdateAsync(clientTest, cancellationTokenSource.Token));
			_mockRepository.Verify(r => r.UpdateAsync(It.IsAny<ClientTest>(), It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		}
	}
}
