using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Services;
using Clean.Sdk.Domain.Tests.Builders;
using Clean.Sdk.Domain.Tests.TestEntites.ClientsTest;
using Clean.Sdk.Domain.Tests.TestServices.Clients;
using Microsoft.Extensions.Logging;
using Moq;

namespace Clean.Sdk.Domain.Tests.TestServices
{
	public class DomainDeletedServiceTest
	{
		private readonly Mock<IRepository<ClientTest>> _mockRepository;
		private readonly Mock<ILogger<Service>> _mockLogger;
		private readonly DeleteClientTestService _service;

		public DomainDeletedServiceTest()
		{
			_mockRepository = new Mock<IRepository<ClientTest>>();
			_mockLogger = new Mock<ILogger<Service>>();

			_service = new DeleteClientTestService(
				_mockLogger.Object,
				new Lazy<IRepository<ClientTest>>(() => _mockRepository.Object));
		}

		[Fact]
		public async Task DeleteByIdAsync_ValidId_ReturnsTrue()
		{
			// Arrange
			var id = Guid.NewGuid();

			_mockRepository
				.Setup(r => r.DeleteByIdAsync(id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(true);

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _service.DeleteByIdAsync(id, CancellationToken.None);

			// Assert
			Assert.True(result);
			_mockRepository.Verify(r => r.DeleteByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task DeleteByIdAsync_InvalidId_ReturnsFalse()
		{
			// Arrange
			var id = Guid.NewGuid();

			_mockRepository
				.Setup(r => r.DeleteByIdAsync(id, It.IsAny<CancellationToken>()))
				.ReturnsAsync(false);

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _service.DeleteByIdAsync(id, CancellationToken.None);

			// Assert
			Assert.False(result);
			_mockRepository.Verify(r => r.DeleteByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		}

		[Fact]
		public async Task DeleteByIdAsync_WithCancellationToken_CancelsOperation()
		{
			// Arrange
			var id = Guid.NewGuid();
			var cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.Cancel();

			_mockRepository
				.Setup(r => r.DeleteByIdAsync(It.IsAny<object>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((object id, CancellationToken cancellationToken) =>
				{
					if (cancellationToken.IsCancellationRequested)
						throw new OperationCanceledException();
					return true;
				});

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Callback((CancellationToken cancellationToken) =>
				{
					if (cancellationToken.IsCancellationRequested)
						throw new OperationCanceledException();
				});

			// Act & Assert
			await Assert.ThrowsAsync<OperationCanceledException>(() => _service.DeleteByIdAsync(id, cancellationTokenSource.Token));
			_mockRepository.Verify(r => r.DeleteByIdAsync(It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		}

		[Fact]
		public async Task DeleteAsync_ValidEntity_ReturnsTrue()
		{
			// Arrange
			var clientTest = new ClientTestBuilder().Build();

			_mockRepository
				.Setup(r => r.DeleteAsync(clientTest, It.IsAny<CancellationToken>()))
				.ReturnsAsync(true);

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _service.DeleteAsync(clientTest, CancellationToken.None);

			// Assert
			Assert.True(result);
			_mockRepository.Verify(r => r.DeleteAsync(clientTest, It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task DeleteAsync_InvalidEntity_ReturnsFalse()
		{
			// Arrange
			var clientTest = new ClientTestBuilder().Build();

			_mockRepository
				.Setup(r => r.DeleteAsync(clientTest, It.IsAny<CancellationToken>()))
				.ReturnsAsync(false);

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _service.DeleteAsync(clientTest, CancellationToken.None);

			// Assert
			Assert.False(result);
			_mockRepository.Verify(r => r.DeleteAsync(clientTest, It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		}

		[Fact]
		public async Task DeleteAsync_WithCancellationToken_CancelsOperation()
		{
			// Arrange
			var clientTest = new ClientTestBuilder().Build();
			var cancellationTokenSource = new CancellationTokenSource();
			cancellationTokenSource.Cancel();

			_mockRepository
				.Setup(r => r.DeleteAsync(It.IsAny<ClientTest>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((object id, CancellationToken cancellationToken) =>
				{
					if (cancellationToken.IsCancellationRequested)
						throw new OperationCanceledException();
					return true;
				});

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Callback((CancellationToken cancellationToken) =>
				{
					if (cancellationToken.IsCancellationRequested)
						throw new OperationCanceledException();
				});

			// Act & Assert
			await Assert.ThrowsAsync<OperationCanceledException>(() => _service.DeleteAsync(clientTest, cancellationTokenSource.Token));
			_mockRepository.Verify(r => r.DeleteAsync(It.IsAny<ClientTest>(), It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		}
	}
}
