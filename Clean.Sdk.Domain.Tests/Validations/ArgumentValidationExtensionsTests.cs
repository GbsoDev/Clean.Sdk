using Clean.Sdk.Domain.Validations;

namespace Clean.Sdk.Domain.Tests.Validations
{
	public class ArgumentValidationExtensionsTests
	{
		private const int DefaultValue = 0;
		private const int NonDefaultValue = 1;
		private const int GreaterValue = 10;
		private const int LesserValue = 5;
		private const float NonDefaultFloatValue = 5.5f;
		private const decimal NonDefaultDecimalValue = 10.5m;
		private const string DefaultStringValue = "";
		private const string NonDefaultStringValue = "hello";
		private static readonly DateTime DefaultDateTimeValue = default;
		private static readonly DateTime NonDefaultDateTimeValue = DateTime.Now;
		private static readonly Guid DefaultGuidValue = default;
		private static readonly Guid NonDefaultGuidValue = Guid.NewGuid();
		private static readonly TimeSpan NonDefaultTimeSpanValue = TimeSpan.FromHours(1);

		[Fact]
		public void IsNotDefault_ShouldReturnFalseForDefaultValue()
		{
			bool result = DefaultValue.IsNotDefault();
			Assert.False(result);
		}

		[Fact]
		public void IsNotDefault_ShouldReturnTrueForNonDefaultValue()
		{
			bool result = NonDefaultValue.IsNotDefault();
			Assert.True(result);
		}

		[Fact]
		public void IsNotDefault_ShouldReturnFalseForDefaultDateTime()
		{
			bool result = DefaultDateTimeValue.IsNotDefault();
			Assert.False(result);
		}

		[Fact]
		public void IsNotDefault_ShouldReturnTrueForNonDefaultDateTime()
		{
			bool result = NonDefaultDateTimeValue.IsNotDefault();
			Assert.True(result);
		}

		[Fact]
		public void IsNotDefault_ShouldReturnFalseForDefaultGuid()
		{
			bool result = DefaultGuidValue.IsNotDefault();
			Assert.False(result);
		}

		[Fact]
		public void IsNotDefault_ShouldReturnTrueForNonDefaultGuid()
		{
			bool result = NonDefaultGuidValue.IsNotDefault();
			Assert.True(result);
		}

		[Fact]
		public void IsGreaterThan_ShouldReturnTrueIfValueIsGreater()
		{
			bool result = GreaterValue.IsGreaterThan(LesserValue);
			Assert.True(result);
		}

		[Fact]
		public void IsGreaterThan_ShouldReturnFalseIfValueIsNotGreater()
		{
			bool result = LesserValue.IsGreaterThan(GreaterValue);
			Assert.False(result);
		}

		[Fact]
		public void IsGreaterOrEqualTo_ShouldReturnTrueIfValueIsGreaterOrEqual()
		{
			bool result = GreaterValue.IsGreaterOrEqualTo(GreaterValue);
			Assert.True(result);
		}

		[Fact]
		public void IsGreaterOrEqualTo_ShouldReturnFalseIfValueIsLess()
		{
			bool result = LesserValue.IsGreaterOrEqualTo(GreaterValue);
			Assert.False(result);
		}

		[Fact]
		public void IsGreaterOrEqualTo_ShouldReturnTrueForDecimal()
		{
			bool result = NonDefaultDecimalValue.IsGreaterOrEqualTo(NonDefaultDecimalValue);
			Assert.True(result);
		}

		[Fact]
		public void IsLessThan_ShouldReturnTrueIfValueIsLess()
		{
			bool result = LesserValue.IsLessThan(GreaterValue);
			Assert.True(result);
		}

		[Fact]
		public void IsLessThan_ShouldReturnFalseIfValueIsNotLess()
		{
			bool result = GreaterValue.IsLessThan(LesserValue);
			Assert.False(result);
		}

		[Fact]
		public void IsLessThan_ShouldReturnTrueForFloat()
		{
			bool result = NonDefaultFloatValue.IsLessThan(GreaterValue);
			Assert.True(result);
		}

		[Fact]
		public void IsLessOrEqualTo_ShouldReturnTrueIfValueIsLessOrEqual()
		{
			bool result = LesserValue.IsLessOrEqualTo(LesserValue);
			Assert.True(result);
		}

		[Fact]
		public void IsLessOrEqualTo_ShouldReturnFalseIfValueIsGreater()
		{
			bool result = GreaterValue.IsLessOrEqualTo(LesserValue);
			Assert.False(result);
		}

		[Fact]
		public void IsLessOrEqualTo_ShouldReturnTrueForTimeSpan()
		{
			bool result = NonDefaultTimeSpanValue.IsLessOrEqualTo(NonDefaultTimeSpanValue);
			Assert.True(result);
		}

		[Fact]
		public void Between_ShouldReturnTrueIfValueIsBetween()
		{
			bool result = LesserValue.Between(LesserValue, GreaterValue);
			Assert.True(result);
		}

		[Fact]
		public void Between_ShouldReturnFalseIfValueIsNotBetween()
		{
			bool result = GreaterValue.Between(LesserValue, LesserValue);
			Assert.False(result);
		}

		[Fact]
		public void Between_ShouldReturnTrueForDateTime()
		{
			DateTime start = NonDefaultDateTimeValue.AddDays(-1);
			DateTime end = NonDefaultDateTimeValue.AddDays(1);
			bool result = NonDefaultDateTimeValue.Between(start, end);
			Assert.True(result);
		}

		[Fact]
		public void LengthBetween_ShouldReturnTrueIfLengthIsBetween()
		{
			int start = 1;
			int end = 10;
			bool result = NonDefaultStringValue.LengthBetween(start, end);
			Assert.True(result);
		}

		[Fact]
		public void LengthBetween_ShouldReturnFalseIfLengthIsNotBetween()
		{
			int start = 6;
			int end = 10;
			bool result = NonDefaultStringValue.LengthBetween(start, end);
			Assert.False(result);
		}

		[Fact]
		public void IsNotNull_ShouldReturnTrueIfNotNull()
		{
			object obj = new object();
			bool result = obj.IsNotNull();
			Assert.True(result);
		}

		[Fact]
		public void IsNotNull_ShouldReturnFalseIfNull()
		{
			object obj = null;
			bool result = obj.IsNotNull();
			Assert.False(result);
		}

		[Fact]
		public void IsNotEmpty_ShouldReturnTrueIfNotEmpty()
		{
			bool result = NonDefaultStringValue.IsNotEmpty();
			Assert.True(result);
		}

		[Fact]
		public void IsNotEmpty_ShouldReturnFalseIfEmpty()
		{
			bool result = DefaultStringValue.IsNotEmpty();
			Assert.False(result);
		}

		[Fact]
		public void IsNotNullOrEmptyWhiteSpace_ShouldReturnTrueIfNotNullOrEmptyWhiteSpace()
		{
			bool result = NonDefaultStringValue.IsNotNullOrEmptyWhiteSpace();
			Assert.True(result);
		}

		[Fact]
		public void IsNotNullOrEmptyWhiteSpace_ShouldReturnFalseIfNullOrWhiteSpace()
		{
			string whiteSpaceString = "   ";
			bool result = whiteSpaceString.IsNotNullOrEmptyWhiteSpace();
			Assert.False(result);
		}
	}
}
