namespace Clean.Domain.Helpers
{
	public static class ValidatorHelper
	{
		public static bool IsNull<T>(this T obj, string message)
			where T : class
		{
			return obj is null;
		}

		public static bool IsEmptyWhiteSpace(this string value)
		{
			return value.Trim() == string.Empty;
		}

		public static bool IsNullOrEmptyWhiteSpace(this string? value)
		{
			return string.IsNullOrWhiteSpace(value);
		}


		public static bool InRange(this string value, short minLenght, short maxLenght)
		{
			return value.Length < minLenght || value.Length > maxLenght;
		}
	}
}
