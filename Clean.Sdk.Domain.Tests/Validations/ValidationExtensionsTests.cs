using Clean.Sdk.Domain.Validations;

namespace Clean.Sdk.Domain.Tests.Validations
{
	public class ValidationExtensionsTests
	{
		private const string ErrorMessage = "Error message";
		private const string FormattedErrorMessage = "Error message with param: {0}";
		private const string Param = "param";
		private const string ExpectedFormattedErrorMessage = "Error message with param: param";

		[Fact]
		public void AddIsNotDefaultValidation_ShouldAddErrorWhenDefault()
		{
			var validationSet = new ValidationSet();
			validationSet.AddIsNotDefaultValidation(default(int), ErrorMessage);
			Assert.Single(validationSet.Errors);
			Assert.Equal(ErrorMessage, validationSet.Errors[0].Message);
		}

		[Fact]
		public void AddIsNotDefaultValidation_ShouldNotAddErrorWhenNotDefault()
		{
			var validationSet = new ValidationSet();
			validationSet.AddIsNotDefaultValidation(1, ErrorMessage);
			Assert.Empty(validationSet.Errors);
		}

		[Fact]
		public void AddGreaterThanValidation_ShouldAddErrorWhenNotGreaterThan()
		{
			var validationSet = new ValidationSet();
			validationSet.AddGreaterThanValidation(1, 2, ErrorMessage);
			Assert.Single(validationSet.Errors);
			Assert.Equal(ErrorMessage, validationSet.Errors[0].Message);
		}

		[Fact]
		public void AddGreaterThanValidation_ShouldNotAddErrorWhenGreaterThan()
		{
			var validationSet = new ValidationSet();
			validationSet.AddGreaterThanValidation(2, 1, ErrorMessage);
			Assert.Empty(validationSet.Errors);
		}

		[Fact]
		public void AddGreaterOrEqualToValidation_ShouldAddErrorWhenNotGreaterOrEqualTo()
		{
			var validationSet = new ValidationSet();
			validationSet.AddGreaterOrEqualToValidation(1, 2, ErrorMessage);
			Assert.Single(validationSet.Errors);
			Assert.Equal(ErrorMessage, validationSet.Errors[0].Message);
		}

		[Fact]
		public void AddGreaterOrEqualToValidation_ShouldNotAddErrorWhenGreaterOrEqualTo()
		{
			var validationSet = new ValidationSet();
			validationSet.AddGreaterOrEqualToValidation(2, 1, ErrorMessage);
			validationSet.AddGreaterOrEqualToValidation(2, 2, ErrorMessage);
			Assert.Empty(validationSet.Errors);
		}

		[Fact]
		public void AddLessThanValidation_ShouldAddErrorWhenNotLessThan()
		{
			var validationSet = new ValidationSet();
			validationSet.AddLessThanValidation(2, 1, ErrorMessage);
			Assert.Single(validationSet.Errors);
			Assert.Equal(ErrorMessage, validationSet.Errors[0].Message);
		}

		[Fact]
		public void AddLessThanValidation_ShouldNotAddErrorWhenLessThan()
		{
			var validationSet = new ValidationSet();
			validationSet.AddLessThanValidation(1, 2, ErrorMessage);
			Assert.Empty(validationSet.Errors);
		}

		[Fact]
		public void AddLessOrEqualToValidation_ShouldAddErrorWhenNotLessOrEqualTo()
		{
			var validationSet = new ValidationSet();
			validationSet.AddLessOrEqualToValidation(2, 1, ErrorMessage);
			Assert.Single(validationSet.Errors);
			Assert.Equal(ErrorMessage, validationSet.Errors[0].Message);
		}

		[Fact]
		public void AddLessOrEqualToValidation_ShouldNotAddErrorWhenLessOrEqualTo()
		{
			var validationSet = new ValidationSet();
			validationSet.AddLessOrEqualToValidation(1, 2, ErrorMessage);
			validationSet.AddLessOrEqualToValidation(2, 2, ErrorMessage);
			Assert.Empty(validationSet.Errors);
		}

		[Fact]
		public void AddBetweenValidation_ShouldAddErrorWhenNotBetween()
		{
			var validationSet = new ValidationSet();
			validationSet.AddBetweenValidation(0, 1, 2, ErrorMessage);
			validationSet.AddBetweenValidation(3, 1, 2, ErrorMessage);
			Assert.Equal(2, validationSet.Errors.Count);
			Assert.Equal(ErrorMessage, validationSet.Errors[0].Message);
			Assert.Equal(ErrorMessage, validationSet.Errors[1].Message);
		}

		[Fact]
		public void AddBetweenValidation_ShouldNotAddErrorWhenBetween()
		{
			var validationSet = new ValidationSet();
			validationSet.AddBetweenValidation(1, 0, 2, ErrorMessage);
			validationSet.AddBetweenValidation(2, 1, 2, ErrorMessage);
			Assert.Empty(validationSet.Errors);
		}

		[Fact]
		public void AddLengthBetweenValidation_ShouldAddErrorWhenLengthNotBetween()
		{
			var validationSet = new ValidationSet();
			validationSet.AddLengthBetweenValidation("a", 2, 3, ErrorMessage);
			validationSet.AddLengthBetweenValidation("abcd", 1, 3, ErrorMessage);
			Assert.Equal(2, validationSet.Errors.Count);
			Assert.Equal(ErrorMessage, validationSet.Errors[0].Message);
			Assert.Equal(ErrorMessage, validationSet.Errors[1].Message);
		}

		[Fact]
		public void AddLengthBetweenValidation_ShouldNotAddErrorWhenLengthBetween()
		{
			var validationSet = new ValidationSet();
			validationSet.AddLengthBetweenValidation("ab", 1, 3, ErrorMessage);
			validationSet.AddLengthBetweenValidation("abc", 2, 3, ErrorMessage);
			Assert.Empty(validationSet.Errors);
		}

		[Fact]
		public void AddIsNotNullValidation_ShouldAddErrorWhenNull()
		{
			var validationSet = new ValidationSet();
			validationSet.AddIsNotNullValidation<string>(null, ErrorMessage);
			Assert.Single(validationSet.Errors);
			Assert.Equal(ErrorMessage, validationSet.Errors[0].Message);
		}

		[Fact]
		public void AddIsNotNullValidation_ShouldNotAddErrorWhenNotNull()
		{
			var validationSet = new ValidationSet();
			validationSet.AddIsNotNullValidation("not null", ErrorMessage);
			Assert.Empty(validationSet.Errors);
		}

		[Fact]
		public void AddIsNotEmptyValidation_ShouldAddErrorWhenEmpty()
		{
			var validationSet = new ValidationSet();
			validationSet.AddIsNotEmptyValidation(string.Empty, ErrorMessage);
			Assert.Single(validationSet.Errors);
			Assert.Equal(ErrorMessage, validationSet.Errors[0].Message);
		}

		[Fact]
		public void AddIsNotEmptyValidation_ShouldNotAddErrorWhenNotEmpty()
		{
			var validationSet = new ValidationSet();
			validationSet.AddIsNotEmptyValidation("not empty", ErrorMessage);
			Assert.Empty(validationSet.Errors);
		}

		[Fact]
		public void AddIsNotEmptyOrWhiteSpaceValidation_ShouldAddErrorWhenEmptyOrWhiteSpace()
		{
			var validationSet = new ValidationSet();
			validationSet.AddIsNotEmptyOrWhiteSpaceValidation(string.Empty, ErrorMessage);
			validationSet.AddIsNotEmptyOrWhiteSpaceValidation(" ", ErrorMessage);
			Assert.Equal(2, validationSet.Errors.Count);
			Assert.Equal(ErrorMessage, validationSet.Errors[0].Message);
			Assert.Equal(ErrorMessage, validationSet.Errors[1].Message);
		}

		[Fact]
		public void AddIsNotEmptyOrWhiteSpaceValidation_ShouldNotAddErrorWhenNotEmptyOrWhiteSpace()
		{
			var validationSet = new ValidationSet();
			validationSet.AddIsNotEmptyOrWhiteSpaceValidation("not empty", ErrorMessage);
			Assert.Empty(validationSet.Errors);
		}

		[Fact]
		public void AddIsDefinedValidation_ShouldAddErrorWhenNotDefinedEnum()
		{
			var validationSet = new ValidationSet();
			validationSet.AddIsDefinedValidation((DayOfWeek)10, ErrorMessage);
			Assert.Single(validationSet.Errors);
			Assert.Equal(ErrorMessage, validationSet.Errors[0].Message);
		}

		[Fact]
		public void AddIsDefinedValidation_ShouldNotAddErrorWhenDefinedEnum()
		{
			var validationSet = new ValidationSet();
			validationSet.AddIsDefinedValidation(DayOfWeek.Monday, ErrorMessage);
			Assert.Empty(validationSet.Errors);
		}

		[Fact]
		public void AddIsParsedValidation_ShouldAddErrorWhenCannotParse()
		{
			var validationSet = new ValidationSet();
			validationSet.AddIsParsedValidation<DayOfWeek>("NotAnEnum", ErrorMessage);
			Assert.Single(validationSet.Errors);
			Assert.Equal(ErrorMessage, validationSet.Errors[0].Message);
		}

		[Fact]
		public void AddIsParsedValidation_ShouldNotAddErrorWhenCanParse()
		{
			var validationSet = new ValidationSet();
			validationSet.AddIsParsedValidation<DayOfWeek>("Monday", ErrorMessage);
			Assert.Empty(validationSet.Errors);
		}

		[Fact]
		public void AddValidation_ShouldAddErrorWhenConditionNotMet()
		{
			var validationSet = new ValidationSet();
			validationSet.AddValidation(1, x => x > 2, ErrorMessage);
			Assert.Single(validationSet.Errors);
			Assert.Equal(ErrorMessage, validationSet.Errors[0].Message);
		}

		[Fact]
		public void AddValidation_ShouldNotAddErrorWhenConditionMet()
		{
			var validationSet = new ValidationSet();
			validationSet.AddValidation(3, x => x > 2, ErrorMessage);
			Assert.Empty(validationSet.Errors);
		}

		[Fact]
		public void AddValidation_WithMessageParams_ShouldFormatErrorMessage()
		{
			var validationSet = new ValidationSet();
			validationSet.AddValidation(false, FormattedErrorMessage, Param);
			Assert.Single(validationSet.Errors);
			Assert.Equal(ExpectedFormattedErrorMessage, validationSet.Errors[0].Message);
		}
	}
}
