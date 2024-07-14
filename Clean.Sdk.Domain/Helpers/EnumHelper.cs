using Clean.Sdk.Domain.Resources;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Clean.Sdk.Domain.Helpers
{
	public static class EnumHelper
	{
		private static class EnumCache<TEnum> where TEnum : struct, Enum
		{
			public static readonly ConcurrentDictionary<string, TEnum> Enums = new ConcurrentDictionary<string, TEnum>();
			public static readonly HashSet<TEnum> TrueValues = new HashSet<TEnum>(Enum.GetValues(typeof(TEnum)).Cast<TEnum>());
		}

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
