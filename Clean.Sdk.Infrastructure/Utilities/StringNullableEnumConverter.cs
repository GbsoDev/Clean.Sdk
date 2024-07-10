using Clean.Sdk.Domain.Helpers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Clean.Sdk.Infrastructure.Utilidades
{
	public class StringNullableEnumConverter<TEnum> : ValueConverter<TEnum?, string?> where TEnum : struct, Enum
	{
		public StringNullableEnumConverter()
			: base(
				enumerador => Tostring(enumerador),
				texto => texto.ToNullableEnum<TEnum>())
		{
		}

		private static string? Tostring(TEnum? enumerador)
		{
			return enumerador?.ToString();
		}
	}
}
