using Clean.Sdk.Data.EfCore;
using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Tests.Builders;
using Clean.Sdk.Domain.Tests.TestEntites.ClientsTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Moq.EntityFrameworkCore;

namespace Clean.Sdk.Infrastructure.Tests
{
	public class EfRepositoryTests
	{
		private readonly Mock<IEfDbContext> _mockContext;
		private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;
		private readonly EfRepository<ClientTest, IEfDbContext> _repository;

		public EfRepositoryTests()
		{
			_mockContext = new Mock<IEfDbContext>();
			_mockDateTimeProvider = new Mock<IDateTimeProvider>();
			_repository = new EfRepository<ClientTest, IEfDbContext>(_mockContext.Object, new Lazy<IDateTimeProvider>(() => _mockDateTimeProvider.Object));
		}

		[Fact]
		public async Task SaveAsync_Should_Add_Entity()
		{
			// Arrange
			var entity = new ClientTestBuilder().BuildToCreate();
			var mockSet = new Mock<DbSet<ClientTest>>();
			_mockContext
				.Setup(c => c.Set<ClientTest>())
				.Returns(mockSet.Object);

			// Act
			var result = await _repository.SaveAsync(entity);

			// Assert
			mockSet.Verify(s => s.AddAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
			Assert.Equal(entity, result);
		}

		[Fact]
		public async Task GetAllAsync_Should_Return_All_Entities()
		{
			// Arrange
			var expectedArray = new[] { new ClientTestBuilder().Build() };
			_mockContext.Setup(c => c.Set<ClientTest>())
				.ReturnsDbSet(expectedArray);

			// Act
			var result = await _repository.GetAllAsync();

			// Assert
			Assert.Equal(expectedArray, result);
		}

		[Fact]
		public async Task GetByIdAsync_Should_Return_Entity_By_Id()
		{
			// Arrange
			var id = Guid.NewGuid();
			var entity = new ClientTestBuilder()
				.WithId(id)
				.Build();

			_mockContext.Setup(c => c.Set<ClientTest>())
				.ReturnsDbSet(new List<ClientTest> { entity });
			_mockContext.Setup(c => c.FindAsync<ClientTest>(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(entity);

			// Act
			var result = await _repository.GetByIdAsync(id);

			// Assert
			Assert.Equal(entity, result);
		}

		[Fact]
		public async Task UpdateAsync_Should_Throw_NotFoundException_When_Entity_Not_Found()
		{
			// Arrange
			var entity = new ClientTestBuilder().Build();
			_mockContext.Setup(c => c.FindAsync<ClientTest>(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((ClientTest?)null);

			// Act & Assert
			await Assert.ThrowsAsync<NotFoundException>(() => _repository.UpdateAsync(entity));
		}

		[Fact]
		public async Task DeleteAsync_Should_Remove_Entity()
		{
			// Arrange
			var entity = new ClientTestBuilder().Build();

			var mockDbSet = new Mock<DbSet<ClientTest>>();

			var listEntity = new List<ClientTest> { entity };


			_mockContext.Setup(c => c.Set<ClientTest>())
				.ReturnsDbSet(listEntity, mockDbSet);


			_mockDateTimeProvider.Setup(p => p.UtcNow)
				.Returns(DateTime.UtcNow);

			var mockEntityEntry = new Mock<EntityEntry<ClientTest>>();
			mockEntityEntry.Setup(e => e.Entity).Returns(entity);
			mockEntityEntry.Setup(e => e.State).Returns(EntityState.Added);

			var entryEntity = mockEntityEntry.Object;

			mockDbSet.Setup(ClientTest => ClientTest.Remove(It.IsAny<ClientTest>()))
				.Returns(entryEntity);

			// Act
			var result = await _repository.DeleteAsync(entity);

			// Assert
			mockDbSet.Verify(s => s.Remove(It.IsAny<ClientTest>()), Times.Once);
			Assert.True(result);
		}

		[Fact]
		public async Task DeleteByIdAsync_Should_Remove_Entity()
		{
			// Arrange
			var entity = new ClientTestBuilder().Build();

			var mockDbSet = new Mock<DbSet<ClientTest>>();

			var listEntity = new List<ClientTest> { entity };

			_mockContext.Setup(c => c.Set<ClientTest>())
				.ReturnsDbSet(listEntity, mockDbSet);

			_mockDateTimeProvider.Setup(p => p.UtcNow)
				.Returns(DateTime.UtcNow);

			// Act
			var result = await _repository.DeleteAsync(entity);

			// Assert
			mockDbSet.Verify(s => s.Remove(It.IsAny<ClientTest>()), Times.Once);
			Assert.True(result);
		}

		[Fact]
		public async Task SaveChangesAsync_Should_Update_Timestamps_And_Save_Changes()
		{
			// Arrange
			var mockSet = new Mock<DbSet<ClientTest>>();
			var mockEntry = new Mock<EntityEntry<ClientTest>>();
			mockEntry.Setup(e => e.State).Returns(EntityState.Added);
			mockEntry.Setup(e => e.Property(It.IsAny<string>())).Returns(Mock.Of<PropertyEntry<ClientTest, DateTime>>);

			_mockContext.Setup(c => c.ChangeTracker.Entries()).Returns(new List<EntityEntry> { mockEntry.Object });
			_mockContext.Setup(c => c.Set<ClientTest>()).Returns(mockSet.Object);
			_mockDateTimeProvider.Setup(p => p.UtcNow).Returns(DateTime.UtcNow);

			// Act
			await _repository.SaveChangesAsync();

			// Assert
			mockEntry.VerifySet(p => p.Property(IEfDbContext.SAVE_DATE_PROPERTY_NAME).CurrentValue = It.IsAny<DateTime>(), Times.Once);
			_mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		}
	}
}
