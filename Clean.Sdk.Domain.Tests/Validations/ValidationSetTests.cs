using Clean.Sdk.Domain.Validations;

namespace Clean.Sdk.Domain.Tests.Validations
{
	public class ValidationSetTests
	{
		private const string TestErrorMessage = "Test error message";
		private const string TestError = "Test error";

		[Fact]
		public void Constructor_ShouldInitializeEmptyErrorList()
		{
			var validationSet = new ValidationSet();
			Assert.Empty(validationSet.Errors);
		}

		[Fact]
		public void Constructor_WithErrorMessage_ShouldSetErrorMessage()
		{
			var validationSet = new ValidationSet(TestErrorMessage);
			Assert.Equal(TestErrorMessage, validationSet.ErrorMessage);
		}

		[Fact]
		public void IsValid_ShouldReturnTrueWhenNoErrors()
		{
			var validationSet = new ValidationSet();
			Assert.True(validationSet.IsValid);
		}

		[Fact]
		public void IsValid_ShouldReturnFalseWhenErrorsExist()
		{
			var validationSet = new ValidationSet();
			validationSet.AddError(TestError);
			Assert.False(validationSet.IsValid);
		}

		[Fact]
		public void AddError_ShouldAddErrorToErrorList()
		{
			var validationSet = new ValidationSet();
			validationSet.AddError(TestError);
			Assert.Single(validationSet.Errors);
			Assert.Equal(TestError, validationSet.Errors.First().Message);
		}

		[Fact]
		public void ValidateAndThrow_ShouldThrowExceptionWhenErrorsExist()
		{
			var validationSet = new ValidationSet(TestErrorMessage);
			validationSet.AddError(TestError);

			var exception = Assert.Throws<ValidationException>(() => validationSet.ValidateAndThrow());
			Assert.Equal(TestErrorMessage, exception.Message);
		}

		[Fact]
		public void ValidateAndThrow_ShouldNotThrowExceptionWhenNoErrors()
		{
			var validationSet = new ValidationSet();
			validationSet.ValidateAndThrow();
		}

		[Fact]
		public void SetErrorMessage_ShouldUpdateErrorMessage()
		{
			var validationSet = new ValidationSet();
			validationSet.SetErrorMessage(TestErrorMessage);
			Assert.Equal(TestErrorMessage, validationSet.ErrorMessage);
		}
	}
}
