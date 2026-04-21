using Clean.Sdk.Domain.Helpers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Clean.Sdk.Infrastructure.Utilidades
{
	/// <summary>
	/// Converts a nullable enum to its string representation and vice versa for Entity Framework Core.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	public class StringNullableEnumConverter<TEnum> : ValueConverter<TEnum?, string?> where TEnum : struct, Enum
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StringNullableEnumConverter{TEnum}"/> class.
		/// </summary>
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
