using Clean.Sdk.Domain.Helpers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
namespace Clean.Sdk.Infrastructure.Utilidades
{
	public class StringEnumConverter<TEnum> : ValueConverter<TEnum, string> where TEnum : struct, Enum
	{
		public StringEnumConverter()
			: base(
				enumerador => enumerador.ToString(),
				texto => texto.ToEnum<TEnum>())
		{
		}
	}
}
