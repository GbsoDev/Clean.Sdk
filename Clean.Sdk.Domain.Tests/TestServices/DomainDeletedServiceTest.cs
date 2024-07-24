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
		public async Task DeleteAsync_ValidEntity_ReturnsTrue()
		{
			// Arrange
			var entity = new ClientTestBuilder().Build();

			_mockRepository
				.Setup(r => r.DeleteAsync(entity, It.IsAny<CancellationToken>()))
				.ReturnsAsync(true);

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _service.DeleteAsync(entity, CancellationToken.None);

			// Assert
			Assert.True(result);
			_mockRepository.Verify(r => r.DeleteAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task DeleteAsync_InvalidEntity_ReturnsFalse()
		{
			// Arrange
			var entity = new ClientTestBuilder().Build();

			_mockRepository
				.Setup(r => r.DeleteAsync(entity, It.IsAny<CancellationToken>()))
				.ReturnsAsync(false);

			_mockRepository
				.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _service.DeleteAsync(entity, CancellationToken.None);

			// Assert
			Assert.False(result);
			_mockRepository.Verify(r => r.DeleteAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
			_mockRepository.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
		}
	}
}
