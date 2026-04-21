namespace Clean.Sdk.Domain.Validations
{
	/// <summary>
	/// Provides extension methods for common argument validation scenarios.
	/// </summary>
	public static class ArgumentValidationExtensions
	{
		/// <summary>
		/// Checks if the object is not the default value for its type.
		/// </summary>
		/// <typeparam name="T">The type of the object.</typeparam>
		/// <param name="obj">The object to check.</param>
		/// <returns>True if the object is not the default value; otherwise, false.</returns>
		public static bool IsNotDefault<T>(this T obj)
		{
			return !EqualityComparer<T>.Default.Equals(obj, default);
		}

		/// <summary>
		/// Checks if the value is strictly greater than the specified start value.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="value">The value to check.</param>
		/// <param name="start">The value to compare against.</param>
		/// <returns>True if the value is greater than the start value; otherwise, false.</returns>
		public static bool IsGreaterThan<T>(this T value, T start) where T : struct, IComparable<T>
		{
			return value.CompareTo(start) > 0;
		}

		/// <summary>
		/// Checks if the value is greater than or equal to the specified start value.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="value">The value to check.</param>
		/// <param name="start">The value to compare against.</param>
		/// <returns>True if the value is greater than or equal to the start value; otherwise, false.</returns>
		public static bool IsGreaterOrEqualTo<T>(this T value, T start) where T : struct, IComparable<T>
		{
			return value.CompareTo(start) >= 0;
		}

		/// <summary>
		/// Checks if the value is strictly less than the specified end value.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="value">The value to check.</param>
		/// <param name="end">The value to compare against.</param>
		/// <returns>True if the value is less than the end value; otherwise, false.</returns>
		public static bool IsLessThan<T>(this T value, T end) where T : struct, IComparable<T>
		{
			return value.CompareTo(end) < 0;
		}

		/// <summary>
		/// Checks if the value is less than or equal to the specified end value.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="value">The value to check.</param>
		/// <param name="end">The value to compare against.</param>
		/// <returns>True if the value is less than or equal to the end value; otherwise, false.</returns>
		public static bool IsLessOrEqualTo<T>(this T value, T end) where T : struct, IComparable<T>
		{
			return value.CompareTo(end) <= 0;
		}

		/// <summary>
		/// Checks if the value is between the specified limits (inclusive).
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="value">The value to check.</param>
		/// <param name="start">The start of the range.</param>
		/// <param name="end">The end of the range.</param>
		/// <returns>True if the value is within the range; otherwise, false.</returns>
		public static bool Between<T>(this T value, T start, T end) where T : struct, IComparable<T>
		{
			return value.CompareTo(start) >= 0 && value.CompareTo(end) <= 0;
		}

		/// <summary>
		/// Checks if the length of the string is between the specified limits (inclusive).
		/// </summary>
		/// <param name="value">The string to check.</param>
		/// <param name="start">The minimum length.</param>
		/// <param name="end">The maximum length.</param>
		/// <returns>True if the string length is within the range; otherwise, false.</returns>
		public static bool LengthBetween(this string value, int start, int end)
		{
			return value.Length >= start && value.Length <= end;
		}

		/// <summary>
		/// Checks if the object is not null.
		/// </summary>
		/// <typeparam name="T">The type of the object.</typeparam>
		/// <param name="obj">The object to check.</param>
		/// <returns>True if the object is not null; otherwise, false.</returns>
		public static bool IsNotNull<T>(this T obj)
		{
			return obj != null;
		}

		/// <summary>
		/// Checks if the string is not null or empty.
		/// </summary>
		/// <param name="value">The string to check.</param>
		/// <returns>True if the string is not null or empty; otherwise, false.</returns>
		public static bool IsNotEmpty(this string value)
		{
			return !string.IsNullOrEmpty(value);
		}

		/// <summary>
		/// Checks if the string is not null, empty, or composed only of white space.
		/// </summary>
		/// <param name="value">The string to check.</param>
		/// <returns>True if the string is not null, empty, or white space; otherwise, false.</returns>
		public static bool IsNotNullOrEmptyWhiteSpace(this string value)
		{
			return !string.IsNullOrWhiteSpace(value);
		}
	}
}
