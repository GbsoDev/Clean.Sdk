using Clean.Sdk.Domain.Helpers;
using Clean.Sdk.Domain.Resources;

namespace Clean.Sdk.Domain.Test.Helpers
{
	public class StringEnumConverTest
	{
		public enum TestEnum : short
		{
			First = 1,
			Second = 2,
			Third = 3
		}

		[Theory]
		[InlineData("First", TestEnum.First)]
		[InlineData("1", TestEnum.First)]
		[InlineData("Third", TestEnum.Third)]
		[InlineData("3", TestEnum.Third)]
		[InlineData(null, null)]
		public void Convert_Text_To_NullableEnum(string? text, TestEnum? enumValue)
		{
			// Arrange

			// Act
			var result = text.ToNullableEnum<TestEnum>();

			// Assert
			Assert.Equal(enumValue, result);
		}

		[Theory]
		[InlineData("Fourth")]
		[InlineData("4")]
		[InlineData("0")]
		[InlineData("")]
		public void Convert_Undefined_Text_Should_Throw_Exception(string text)
		{
			// Arrange
			var typeName = nameof(TestEnum);
			var expectedMessage = string.Format(Messages.ErrorNotFoundEnumValue, text, typeName);

			// Act
			var exception = Assert.Throws<ArgumentException>(() => text.ToNullableEnum<TestEnum>());

			// Assert
			Assert.NotNull(exception.Message);
			Assert.NotEmpty(exception.Message);
			Assert.Equal(expectedMessage, exception.Message);
		}

		[Fact]
		public void Convert_Null_Text_Should_Not_Throw_Exception()
		{
			// Arrange
			string? text = null;

			// Act
			var result = text.ToNullableEnum<TestEnum>();

			// Assert
			Assert.Null(result);
		}

		[Theory]
		[InlineData("First", TestEnum.First)]
		[InlineData("1", TestEnum.First)]
		[InlineData("Third", TestEnum.Third)]
		[InlineData("3", TestEnum.Third)]
		public void Convert_Text_ToEnum(string text, TestEnum enumeration)
		{
			// Act
			var result = text.ToEnum<TestEnum>();

			// Assert
			Assert.Equal(enumeration, result);
		}

		[Fact]
		public void Convert_Null_Text_Should_Throw_Exception()
		{
			// Arrange
			string? text = null;
			var typeName = nameof(TestEnum);
			var expectedMessage = string.Format(Messages.ErrorParseEnum, text, typeName);

			// Act
			var exception = Assert.Throws<ArgumentException>(() => text!.ToEnum<TestEnum>());

			// Assert
			Assert.NotNull(exception.Message);
			Assert.NotEmpty(exception.Message);
			Assert.Equal(expectedMessage, exception.Message);
		}
	}
}
