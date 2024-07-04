using System;

namespace Clean.Domain.Validations
{
	public static class validationExtensions
	{
		public static bool Between(this int value, Range range)
		{
			
			return value >= range.Start.Value && value <= range.End.Value;
		}

		public static ValidationSet AddValidation<T>(this ValidationSet validationSet, T objeto, Func<T, bool> condition, string message, out bool isTrue)
		{
			var validation = condition.Invoke(objeto);
			if (!validation)
			{
				validationSet.AddError(message);
			}
			isTrue = validation;
			return validationSet;
		}

		public static ValidationSet AddValidation<T>(this ValidationSet validationSet, T objeto, Func<T, bool> condition, string message)
		{
			var validation = condition.Invoke(objeto);
			if (!validation)
			{
				validationSet.AddError(message);
			}
			return validationSet;
		}
	}
}
