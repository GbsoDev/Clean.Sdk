using AutoMapper;
using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Ports;
using Clean.Sdk.Domain.Tests.Builders;
using Clean.Sdk.Domain.Tests.TestModel.ClientsTest;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Clean.Sdk.Data.EfCore.Tests
{
    public class EfRepositoryTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IEfDbContext> _mockContext;
        private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;
        private readonly EfRepository<ClientTest, ClientTestEntity, IEfDbContext> _repository;

        public EfRepositoryTests()
        {
            var mapperConfiguration = new MapperConfiguration(configure => configure.AddProfile<ClientTestEntityMappingProfile>());
            _mapper = mapperConfiguration.CreateMapper();
            _mockContext = new Mock<IEfDbContext>();
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
            _repository = new EfRepository<ClientTest, ClientTestEntity, IEfDbContext>(
                _mockContext.Object,
                new Lazy<IMapper>(() => _mapper),
                new Lazy<IDateTimeProvider>(() => _mockDateTimeProvider.Object)
            );
        }

        [Fact]
        public async Task SaveAsync_Should_Add_Entity()
        {
            // Arrange
            var model = new ClientTestBuilder().BuildToCreate();
            var entity = _mapper.Map<ClientTestEntity>(model);
            var mockSet = new Mock<DbSet<ClientTestEntity>>();
            _mockContext
                .Setup(c => c.Set<ClientTestEntity>())
                .ReturnsDbSet(new List<ClientTestEntity> { entity }, mockSet);

            // Act
            var result = await _repository.SaveAsync(model);

            // Assert
            mockSet.Verify(s => s.AddAsync(It.IsAny<ClientTestEntity>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.NotNull(result);
            Assert.IsAssignableFrom<ClientTest>(result);
            Assert.Equal(model.Name, result.Name);
            Assert.Equal(model.MiddleName, result.MiddleName);
            Assert.Equal(model.Surname, result.Surname);
            Assert.Equal(model.Age, result.Age);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Entities()
        {
            // Arrange
            var clientsModel = new[] {
                new ClientTestBuilder().Build(),
                new ClientTestBuilder().Build(),
                new ClientTestBuilder().Build()
            };
            var expectedArray = _mapper.Map<ClientTestEntity[]>(clientsModel);
            var mockSet = new Mock<DbSet<ClientTestEntity>>();
            _mockContext
                .Setup(context => context.Set<ClientTestEntity>())
                .ReturnsDbSet(expectedArray, mockSet);

            // Act
            var resultArray = await _repository.GetAllAsync();

            // Assert
            Assert.IsAssignableFrom<ClientTest[]>(resultArray);
            Assert.Equal(expectedArray.Length, resultArray.Length);
            Assert.All(resultArray, result => expectedArray.Any(expected =>
                expected.Id == result.Id
                && expected.Name == result.Name
                && expected.MiddleName == result.MiddleName
                && expected.Surname == result.Surname
                && expected.Age == result.Age)
            );
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Entity_By_Id()
        {
            // Arrange
            var id = Guid.NewGuid();
            var clientModel = new ClientTestBuilder()
                .WithId(id)
                .Build();
            var clientEntity = _mapper.Map<ClientTestEntity>(clientModel);

            _mockContext
                .Setup(c => c.Set<ClientTestEntity>())
                .ReturnsDbSet(new List<ClientTestEntity> { clientEntity });
            _mockContext
                .Setup(c => c.FindAsync<ClientTestEntity>(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(clientEntity);

            // Act
            var result = await _repository.GetByIdAsync(id);

            // Assert
            Assert.IsAssignableFrom<ClientTest>(result);
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_NotFoundException_When_Entity_Not_Found()
        {
            // Arrange
            var model = new ClientTestBuilder().Build();
            _mockContext
                .Setup(c => c.FindAsync<ClientTestEntity>(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((ClientTestEntity?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _repository.UpdateAsync(model));
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Entity()
        {
            // Arrange
            var clientModel = new ClientTestBuilder().Build();
            var clientEntity = _mapper.Map<ClientTestEntity>(clientModel);

            var entities = new List<ClientTestEntity> { clientEntity };

            var mockSet = new Mock<DbSet<ClientTestEntity>>();

            _mockContext.Setup(context => context.Set<ClientTestEntity>())
                .ReturnsDbSet(entities, mockSet);

            _mockDateTimeProvider.Setup(provider => provider.UtcNow)
                .Returns(DateTime.UtcNow);

            // Act
            var result = await _repository.DeleteAsync(clientModel);

            // Assert
            mockSet.Verify(s => s.Remove(It.IsAny<ClientTestEntity>()), Times.Once);
        }

        [Fact]
        public async Task DeleteByIdAsync_Should_Remove_Entity()
        {
            // Arrange
            var clientModel = new ClientTestBuilder().Build();
            var clientEntity = _mapper.Map<ClientTestEntity>(clientModel);

            var entities = new List<ClientTestEntity> { clientEntity };

            var mockSet = new Mock<DbSet<ClientTestEntity>>();
            mockSet.Setup(set => set.Remove(It.IsAny<ClientTestEntity>()));

            _mockContext.Setup(context => context.Set<ClientTestEntity>())
                .ReturnsDbSet(entities, mockSet);

            _mockContext
                .Setup(context => context.FindAsync<ClientTestEntity>(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(clientEntity);

            _mockDateTimeProvider.Setup(p => p.UtcNow)
                .Returns(DateTime.UtcNow);

            // Act
            var result = await _repository.DeleteByIdAsync(clientModel.Id);

            // Assert
            mockSet.Verify(s => s.Remove(It.IsAny<ClientTestEntity>()), Times.Once);
        }

    }
}
