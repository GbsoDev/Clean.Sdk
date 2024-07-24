using Clean.Sdk.Domain.Tests.Builders;
using Clean.Sdk.Domain.Tests.TestEntites.ClientsTest;
using Clean.Sdk.Domain.Validations;

namespace Clean.Sdk.Domain.Tests.TestEntites
{
    public class DomainEntityTest
    {
        private const string validName = "gerson";
        private const string validMiddleName = "brain";
        private const string validsurName = "sanchez";
        private const short validAge = 50;

        private const string textWith60Charters = "Lorem ipsum dolor sit amet, consectetur adipiscing elit est.";
        private static readonly string expectedCreateValidationExceptionMessage = string.Format(ValidationErrorMessages.InvalidEntityToCreate, nameof(ClientTest));
        private static readonly string expectedUpdateValidationExceptionMessage = string.Format(ValidationErrorMessages.InvalidEntityToUpdate, nameof(ClientTest));

        private static readonly string expectedIdValidationErrorMessage = string.Format(ValidationErrorMessages.Required, nameof(ClientTest.Id));
        private static readonly string expectedNameValidationErrorMessage = string.Format(ValidationErrorMessages.Required, nameof(ClientTest.Name));
        private static readonly string expectedNameRangeValidationErrorMessage = string.Format(ValidationErrorMessages.Range, nameof(ClientTest.Name), ClientParameters.NameMinLength, ClientParameters.NameMaxLength);
        private static readonly string expectedMiddleNameRangeValidationErrorMessage = string.Format(ValidationErrorMessages.Range, nameof(ClientTest.MiddleName), ClientParameters.NameMinLength, ClientParameters.NameMaxLength);
        private static readonly string expectedSurnameValidationErrorMessage = string.Format(ValidationErrorMessages.Required, nameof(ClientTest.Surname));
        private static readonly string expectedSurnameRangeValidationErrorMessage = string.Format(ValidationErrorMessages.Range, nameof(ClientTest.Surname), ClientParameters.SurnameMinLength, ClientParameters.SurnameMaxLength);
        private static readonly string expectedAgeValidationErrorMessage = string.Format(ValidationErrorMessages.MinimumAge, ClientParameters.EageMin);


        [Fact]
        public void ValidateCreateEntity_Ok()
        {
            // Arrange
            var expectedName = validName;
            string? expectedMiddleName = null;
            var expectedSurname = validsurName;
            var expectedAge = validAge;

            var clienteBuilder = new ClientTestBuilder()
                .WithName(expectedName)
                .WithMiddleName(expectedMiddleName)
                .WithSurname(expectedSurname)
                .WithAge(expectedAge);

            // Act
            var newCLiente = clienteBuilder.BuildToCreate();

            // Assert
            Assert.Equal(expectedName, newCLiente.Name);
            Assert.Equal(expectedMiddleName, newCLiente.MiddleName);
            Assert.Equal(expectedSurname, newCLiente.Surname);
            Assert.Equal(expectedAge, newCLiente.Age);
        }

        [Fact]
        public void ValidateCreateEntity_Error()
        {
            // Arrange
            const int expectedNumValidationErrors = 6;

            var clienteBuilder = new ClientTestBuilder()
                .WithName(string.Empty)
                .WithMiddleName(textWith60Charters)
                .WithSurname(string.Empty)
                .WithAge(5);

            // Act
            var exception = Assert.Throws<ValidationException>(() =>
            {
                clienteBuilder.BuildToCreate();
            });

            // Assert
            Assert.Equal(expectedCreateValidationExceptionMessage, exception.Message);
            Assert.True(exception.Errors.Length == expectedNumValidationErrors);
            Assert.Collection(exception.Errors,
                error => Assert.Equal(expectedNameValidationErrorMessage, error.Message),
                error => Assert.Equal(expectedNameRangeValidationErrorMessage, error.Message),
                error => Assert.Equal(expectedMiddleNameRangeValidationErrorMessage, error.Message),
                error => Assert.Equal(expectedSurnameValidationErrorMessage, error.Message),
                error => Assert.Equal(expectedSurnameRangeValidationErrorMessage, error.Message),
                error => Assert.Equal(expectedAgeValidationErrorMessage, error.Message)
            );
        }

        [Fact]
        public void ValidateUpdateEntity_Ok()
        {
            // Arrange
            var expectedId = Guid.NewGuid();
            var expectedName = validName;
            string? expectedMiddleName = validMiddleName;
            var expectedSurname = validsurName;
            var expectedAge = validAge;

            var clienteBuilder = new ClientTestBuilder()
                .WithId(expectedId)
                .WithName(expectedName)
                .WithMiddleName(expectedMiddleName)
                .WithSurname(expectedSurname)
                .WithAge(expectedAge);

            // Act
            var cliente = clienteBuilder.Build();

            // Assert
            Assert.Equal(expectedId, cliente.Id);
            Assert.Equal(expectedName, cliente.Name);
            Assert.Equal(expectedMiddleName, cliente.MiddleName);
            Assert.Equal(expectedSurname, cliente.Surname);
            Assert.Equal(expectedAge, cliente.Age);

        }

        [Fact]
        public void ValidateUpdateEntity_Error()
        {
            // Arrange
            const int expectedNumValidationErrors = 6;

            var clienteBuilder = new ClientTestBuilder()
                .WithId(Guid.Empty)
                .WithName(string.Empty)
                .WithMiddleName(string.Empty)
                .WithSurname(textWith60Charters)
                .WithAge(5);

            // Act
            var exception = Assert.Throws<ValidationException>(() =>
            {
                clienteBuilder.Build();
            });

            // Assert
            Assert.Equal(expectedUpdateValidationExceptionMessage, exception.Message);
            Assert.True(exception.Errors.Length == expectedNumValidationErrors);
            Assert.Collection(exception.Errors,
                error => Assert.Equal(expectedNameValidationErrorMessage, error.Message),
                error => Assert.Equal(expectedNameRangeValidationErrorMessage, error.Message),
                error => Assert.Equal(expectedMiddleNameRangeValidationErrorMessage, error.Message),
                error => Assert.Equal(expectedSurnameRangeValidationErrorMessage, error.Message),
                error => Assert.Equal(expectedAgeValidationErrorMessage, error.Message),
                error => Assert.Equal(expectedIdValidationErrorMessage, error.Message)
            );
        }
    }
}
