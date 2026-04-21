using Clean.Sdk.Domain.Helpers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
namespace Clean.Sdk.Infrastructure.Utilidades
{
	/// <summary>
	/// Converts an enum to its string representation and vice versa for Entity Framework Core.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	public class StringEnumConverter<TEnum> : ValueConverter<TEnum, string> where TEnum : struct, Enum
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StringEnumConverter{TEnum}"/> class.
		/// </summary>
		public StringEnumConverter()
			: base(
				enumerador => enumerador.ToString(),
				texto => texto.ToEnum<TEnum>())
		{
		}
	}
}
