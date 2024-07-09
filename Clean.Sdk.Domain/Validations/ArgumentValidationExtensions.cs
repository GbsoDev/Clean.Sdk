using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Domain.Validations
{
	public static class ArgumentValidationExtensions
	{
		/// <summary>
		/// Checks if the value is between the specified limits (inclusive).
		/// </summary>
		public static bool IsGreaterThan<T>(this T value, T start) where T : struct, IComparable<T>
		{
			return value.CompareTo(start) > 0;
		}

		/// <summary>
		/// Checks if the value is between the specified limits (inclusive).
		/// </summary>
		public static bool IsGreaterOrEqualTo<T>(this T value, T start) where T : struct, IComparable<T>
		{
			return value.CompareTo(start) >= 0;
		}

		/// <summary>
		/// Checks if the value is between the specified limits (inclusive).
		/// </summary>
		public static bool IsLessThan<T>(this T value, T end) where T : struct, IComparable<T>
		{
			return value.CompareTo(end) < 0;
		}

		/// <summary>
		/// Checks if the value is between the specified limits (inclusive).
		/// </summary>
		public static bool IsLessOrEqualTo<T>(this T value, T end) where T : struct, IComparable<T>
		{
			return value.CompareTo(end) <= 0;
		}

		/// <summary>
		/// Checks if the value is between the specified limits (inclusive).
		/// </summary>
		public static bool Between<T>(this T value, T start, T end) where T : struct, IComparable<T>
		{
			return value.CompareTo(start) >= 0 && value.CompareTo(end) <= 0;
		}

		/// <summary>
		/// Checks if the length of the string is between the specified limits (inclusive).
		/// </summary>
		public static bool LengthBetween(this string value, int start, int end)
		{
			return value.Length >= start && value.Length <= end;
		}

		/// <summary>
		/// Checks if the object is not null.
		/// </summary>
		public static bool IsNotNull<T>(this T obj)
		{
			return obj != null;
		}

		/// <summary>
		/// Checks if the string is not empty.
		/// </summary>
		public static bool IsNotEmpty(this string value)
		{
			return !string.IsNullOrEmpty(value);
		}

		/// <summary>
		/// Checks if the string is not empty or composed only of white spaces.
		/// </summary>
		public static bool IsNotNullOrEmptyWhiteSpace(this string value)
		{
			return !string.IsNullOrWhiteSpace(value);
		}
	}
}
