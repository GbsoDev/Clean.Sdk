using Clean.Sdk.Domain.Resources;
using System.Collections.Concurrent;

namespace Clean.Sdk.Domain.Helpers
{
	/// <summary>
	/// Helper class for working with enums.
	/// </summary>
	public static class EnumHelper
	{
		private static class EnumCache<TEnum> where TEnum : struct, Enum
		{
			public static readonly ConcurrentDictionary<string, TEnum> Enums = new ConcurrentDictionary<string, TEnum>();
			public static readonly HashSet<TEnum> TrueValues = new HashSet<TEnum>(Enum.GetValues(typeof(TEnum)).Cast<TEnum>());
		}

		/// <summary>
		/// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
		/// </summary>
		/// <typeparam name="TEnum">The enumeration type to which to convert <paramref name="stringValue"/>.</typeparam>
		/// <param name="stringValue">The string representation of the enumeration name or underlying value to convert.</param>
		/// <returns>An object of type <typeparamref name="TEnum"/> whose value is represented by <paramref name="stringValue"/>.</returns>
		/// <exception cref="ArgumentException">Thrown when <paramref name="stringValue"/> is null or cannot be parsed to <typeparamref name="TEnum"/>.</exception>
		public static TEnum ToEnum<TEnum>(this string stringValue)
			where TEnum : struct, Enum
		{
			var typeName = typeof(TEnum).Name;

			if (stringValue == null)
			{
				throw new ArgumentException(string.Format(Messages.ErrorParseEnum, stringValue, typeName));
			}

			return TryParse<TEnum>(stringValue);
		}

		/// <summary>
		/// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent nullable enumerated object.
		/// </summary>
		/// <typeparam name="TEnum">The enumeration type to which to convert <paramref name="stringValue"/>.</typeparam>
		/// <param name="stringValue">The string representation of the enumeration name or underlying value to convert.</param>
		/// <returns>An object of type <typeparamref name="TEnum"/> whose value is represented by <paramref name="stringValue"/>, or null if <paramref name="stringValue"/> is null.</returns>
		public static TEnum? ToNullableEnum<TEnum>(this string? stringValue)
			where TEnum : struct, Enum
		{

			if (stringValue == null)
			{
				return null;
			}

			return TryParse<TEnum>(stringValue);
		}

		private static TEnum TryParse<TEnum>(string stringValue) where TEnum : struct, Enum
		{
			var typeName = typeof(TEnum).Name;

			if (EnumCache<TEnum>.Enums.TryGetValue(stringValue, out TEnum enumValue))
			{
				return enumValue;
			}

			if (!Enum.TryParse(stringValue, out enumValue) || !EnumCache<TEnum>.TrueValues.Contains(enumValue))
			{
				throw new ArgumentException(string.Format(Messages.ErrorNotFoundEnumValue, stringValue, typeName));
			}

			EnumCache<TEnum>.Enums[stringValue] = enumValue;
			return enumValue;
		}
	}
}
