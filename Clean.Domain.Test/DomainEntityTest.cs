using Clean.Domain.Entity.Test.TestModel;
using Clean.Domain.Tests.Builders;
using Clean.Domain.Validations;

namespace Clean.Domain.Entity.Test
{
	public class DomainEntityTest
	{
		[Fact]
		public void ValidateCreateEntity_Ok()
		{
			// Arrange
			var newName = "New Name";
			var newEmail = "New@Email.com";
			var userBuilder = new UserBuilder()
				.WithId(Guid.Empty)
				.WithName(newName)
				.WithEmail(newEmail);

			// Act
			var newUser = userBuilder.Build(false);

			// Assert
			Assert.Equal(newName, newUser.Name);
			Assert.Equal(newEmail, newUser.Email);
		}

		[Fact]
		public void ValidateCreateEntity_Error()
		{
			// Arrange
			var expectedValidationExceptionMessage = string.Format(EntityValidationMessages.InvalidEntityToCreate, nameof(User));
			var expectedNameValidationErrorMessage = string.Format(EntityValidationMessages.RequiredValidationError, nameof(User.Name));
			var expectedEmailValidationErrorMessage = string.Format(EntityValidationMessages.RequiredValidationError, nameof(User.Email));
			const int expectedNumValidationErrors = 2;

			var userBuilder = new UserBuilder()
				.WithId(Guid.Empty)
				.WithName(string.Empty)
				.WithEmail(" ");

			// Act
			var exception = Assert.Throws<EntityValidationException>(() =>
			{
				userBuilder.Build(false);
			});

			// Assert
			Assert.Equal(expectedValidationExceptionMessage, exception.Message);
			Assert.Contains(exception.Errors, validationError => expectedNameValidationErrorMessage.Equals(validationError.Message));
			Assert.Contains(exception.Errors, validationError => expectedEmailValidationErrorMessage.Equals(validationError.Message));
			Assert.True(exception.Errors.Length == expectedNumValidationErrors);
		}

		[Fact]
		public void ValidateUpdateEntity_Ok()
		{
			// Arrange
			var id = Guid.NewGuid();
			var newName = "New Name";
			var newEmail = "New@Email.com";

			var userBuilder = new UserBuilder()
				.WithId(id)
				.WithName(newName)
				.WithEmail(newEmail);

			// Act
			var user = userBuilder.Build(true);

			// Assert
			Assert.Equal(id, user.Id);
			Assert.Equal(newName, user.Name);
			Assert.Equal(newEmail, user.Email);
		}

		[Fact]
		public void ValidateUpdateEntity_Error()
		{
			// Arrange
			var id = Guid.Empty;
			var newName = string.Empty;
			var newEmail = "  ";

			var expectedValidationExceptionMessage = string.Format(EntityValidationMessages.InvalidEntityToUpdate, nameof(User));
			var expectedIdValidationErrorMessage = string.Format(EntityValidationMessages.RequiredValidationError, nameof(User.Id));
			var expectedNameValidationErrorMessage = string.Format(EntityValidationMessages.RequiredValidationError, nameof(User.Name));
			var expectedEmailValidationErrorMessage = string.Format(EntityValidationMessages.RequiredValidationError, nameof(User.Email));
			const int expectedNumValidationErrors = 3;

			// Act
			var userBuilder = new UserBuilder()
				.WithId(id)
				.WithName(newName)
				.WithEmail(newEmail);

			var exception = Assert.Throws<EntityValidationException>(() =>
			{
				userBuilder.Build(true);
			});

			// Assert
			Assert.Equal(expectedValidationExceptionMessage, exception.Message);
			Assert.Contains(exception.Errors, validationError => expectedIdValidationErrorMessage.Equals(validationError.Message));
			Assert.Contains(exception.Errors, validationError => expectedNameValidationErrorMessage.Equals(validationError.Message));
			Assert.Contains(exception.Errors, validationError => expectedEmailValidationErrorMessage.Equals(validationError.Message));
			Assert.True(exception.Errors.Length == expectedNumValidationErrors);
		}
	}
}
