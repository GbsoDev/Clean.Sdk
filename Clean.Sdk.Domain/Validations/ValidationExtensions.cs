using System;

namespace Clean.Sdk.Domain.Validations
{
	public static class ValidationExtensions
	{
		/// <summary>
		/// Checks if the stringValue is greater than the specified start stringValue and adds an error message if it is not.
		/// </summary>
		/// <typeparam name="T">The type of the stringValue being validated, which must be a struct and implement IComparable.</typeparam>
		/// <param name="validationSet">The ValidationSet instance to add the validation to.</param>
		/// <param name="value">The stringValue to validate.</param>
		/// <param name="start">The start stringValue to compare against.</param>
		/// <param name="messageResource">The resource string for the error message.</param>
		/// <param name="messageParams">The parameters to format the message.</param>
		/// <returns>The updated ValidationSet instance.</returns>
		public static ValidationSet AddGreaterThanValidation<T>(this ValidationSet validationSet, T value, T start, string messageResource, params object[] messageParams) where T : struct, IComparable<T>
		{
			return validationSet.AddValidation(value.IsGreaterThan(start), messageResource, messageParams);
		}

		/// <summary>
		/// Checks if the stringValue is greater than or equal to the specified start stringValue and adds an error message if it is not.
		/// </summary>
		/// <typeparam name="T">The type of the stringValue being validated, which must be a struct and implement IComparable.</typeparam>
		/// <param name="validationSet">The ValidationSet instance to add the validation to.</param>
		/// <param name="value">The stringValue to validate.</param>
		/// <param name="start">The start stringValue to compare against.</param>
		/// <param name="messageResource">The resource string for the error message.</param>
		/// <param name="messageParams">The parameters to format the message.</param>
		/// <returns>The updated ValidationSet instance.</returns>
		public static ValidationSet AddGreaterOrEqualToValidation<T>(this ValidationSet validationSet, T value, T start, string messageResource, params object[] messageParams) where T : struct, IComparable<T>
		{
			return validationSet.AddValidation(value.IsGreaterOrEqualTo(start), messageResource, messageParams);
		}

		/// <summary>
		/// Checks if the stringValue is less than the specified end stringValue and adds an error message if it is not.
		/// </summary>
		/// <typeparam name="T">The type of the stringValue being validated, which must be a struct and implement IComparable.</typeparam>
		/// <param name="validationSet">The ValidationSet instance to add the validation to.</param>
		/// <param name="value">The stringValue to validate.</param>
		/// <param name="end">The end stringValue to compare against.</param>
		/// <param name="messageResource">The resource string for the error message.</param>
		/// <param name="messageParams">The parameters to format the message.</param>
		/// <returns>The updated ValidationSet instance.</returns>
		public static ValidationSet AddLessThanValidation<T>(this ValidationSet validationSet, T value, T end, string messageResource, params object[] messageParams) where T : struct, IComparable<T>
		{
			return validationSet.AddValidation(value.IsLessThan(end), messageResource, messageParams);
		}

		/// <summary>
		/// Checks if the stringValue is less than or equal to the specified end stringValue and adds an error message if it is not.
		/// </summary>
		/// <typeparam name="T">The type of the stringValue being validated, which must be a struct and implement IComparable.</typeparam>
		/// <param name="validationSet">The ValidationSet instance to add the validation to.</param>
		/// <param name="value">The stringValue to validate.</param>
		/// <param name="end">The end stringValue to compare against.</param>
		/// <param name="messageResource">The resource string for the error message.</param>
		/// <param name="messageParams">The parameters to format the message.</param>
		/// <returns>The updated ValidationSet instance.</returns>
		public static ValidationSet AddLessOrEqualToValidation<T>(this ValidationSet validationSet, T value, T end, string messageResource, params object[] messageParams) where T : struct, IComparable<T>
		{
			return validationSet.AddValidation(value.IsLessOrEqualTo(end), messageResource, messageParams);
		}

		/// <summary>
		/// Checks if the stringValue is between the specified limits (inclusive) and adds an error message if it is not.
		/// </summary>
		/// <typeparam name="T">The type of the stringValue being validated, which must be a struct and implement IComparable.</typeparam>
		/// <param name="validationSet">The ValidationSet instance to add the validation to.</param>
		/// <param name="value">The stringValue to validate.</param>
		/// <param name="start">The start stringValue of the range.</param>
		/// <param name="end">The end stringValue of the range.</param>
		/// <param name="messageResource">The resource string for the error message.</param>
		/// <param name="messageParams">The parameters to format the message.</param>
		/// <returns>The updated ValidationSet instance.</returns>
		public static ValidationSet AddBetweenValidation<T>(this ValidationSet validationSet, T value, T start, T end, string messageResource, params object[] messageParams) where T : struct, IComparable<T>
		{
			return validationSet.AddValidation(value.Between(start, end), messageResource, messageParams);
		}

		/// <summary>
		/// Checks if the length of the string is between the specified limits (inclusive) and adds an error message if it is not.
		/// </summary>
		/// <param name="validationSet">The ValidationSet instance to add the validation to.</param>
		/// <param name="value">The string stringValue to validate.</param>
		/// <param name="start">The minimum length of the string.</param>
		/// <param name="end">The maximum length of the string.</param>
		/// <param name="messageResource">The resource string for the error message.</param>
		/// <param name="messageParams">The parameters to format the message.</param>
		/// <returns>The updated ValidationSet instance.</returns>
		public static ValidationSet AddLengthBetweenValidation(this ValidationSet validationSet, string value, int start, int end, string messageResource, params object[] messageParams)
		{
			return validationSet.AddValidation(value.LengthBetween(start, end), messageResource, messageParams);
		}

		/// <summary>
		/// Checks if the object is not null and adds an error message if it is.
		/// </summary>
		/// <typeparam name="T">The type of the object being validated.</typeparam>
		/// <param name="validationSet">The ValidationSet instance to add the validation to.</param>
		/// <param name="obj">The object to validate.</param>
		/// <param name="messageResource">The resource string for the error message.</param>
		/// <param name="messageParams">The parameters to format the message.</param>
		/// <returns>The updated ValidationSet instance.</returns>
		public static ValidationSet AddIsNotNullValidation<T>(this ValidationSet validationSet, T obj, string messageResource, params object[] messageParams)
		{
			return validationSet.AddValidation(obj.IsNotNull(), messageResource, messageParams);
		}

		/// <summary>
		/// Checks if the string is not empty and adds an error message if it is.
		/// </summary>
		/// <param name="validationSet">The ValidationSet instance to add the validation to.</param>
		/// <param name="value">The string stringValue to validate.</param>
		/// <param name="messageResource">The resource string for the error message.</param>
		/// <param name="messageParams">The parameters to format the message.</param>
		/// <returns>The updated ValidationSet instance.</returns>
		public static ValidationSet AddIsNotEmptyValidation(this ValidationSet validationSet, string value, string messageResource, params object[] messageParams)
		{
			return validationSet.AddValidation(value.IsNotEmpty(), messageResource, messageParams);
		}

		/// <summary>
		/// Checks if the string is not empty or composed only of white spaces and adds an error message if it is.
		/// </summary>
		/// <param name="validationSet">The ValidationSet instance to add the validation to.</param>
		/// <param name="value">The string stringValue to validate.</param>
		/// <param name="messageResource">The resource string for the error message.</param>
		/// <param name="messageParams">The parameters to format the message.</param>
		/// <returns>The updated ValidationSet instance.</returns>
		public static ValidationSet AddIsNotEmptyOrWhiteSpaceValidation(this ValidationSet validationSet, string value, string messageResource, params object[] messageParams)
		{
			return validationSet.AddValidation(value.IsNotNullOrEmptyWhiteSpace(), messageResource, messageParams);
		}

		/// <summary>
		/// Checks if the enum stringValue is defined in the enum type and adds an error message if it is not.
		/// </summary>
		/// <typeparam name="Tenum">The enum type being validated.</typeparam>
		/// <param name="validationSet">The ValidationSet instance to add the validation to.</param>
		/// <param name="value">The enum stringValue to validate.</param>
		/// <param name="messageResource">The resource string for the error message.</param>
		/// <param name="messageParams">The parameters to format the message.</param>
		/// <returns>The updated ValidationSet instance.</returns>
		public static ValidationSet AddIsDefinedValidation<Tenum>(this ValidationSet validationSet, Tenum value, string messageResource, params object[] messageParams) where Tenum : struct, Enum
		{
			return validationSet.AddValidation(Enum.IsDefined(typeof(Tenum), value), messageResource, messageParams);
		}

		/// <summary>
		/// Checks if the string can be parsed into the specified enum type and adds an error message if it cannot.
		/// </summary>
		/// <typeparam name="Tenum">The enum type to parse the string into.</typeparam>
		/// <param name="validationSet">The ValidationSet instance to add the validation to.</param>
		/// <param name="stringValue">The string stringValue to validate.</param>
		/// <param name="messageResource">The resource string for the error message.</param>
		/// <param name="messageParams">The parameters to format the message.</param>
		/// <returns>The updated ValidationSet instance.</returns>
		public static ValidationSet AddIsParsedValidation<Tenum>(this ValidationSet validationSet, string stringValue, string messageResource, params object[] messageParams) where Tenum : struct, Enum
		{
			return validationSet.AddValidation(Enum.TryParse<Tenum>(stringValue, out _), messageResource, messageParams);
		}

		/// <summary>
		/// Checks if a condition is met and adds an error message if it is not.
		/// </summary>
		/// <typeparam name="T">The type of the object being validated.</typeparam>
		/// <param name="validationSet">The ValidationSet instance to add the validation to.</param>
		/// <param name="obj">The object to validate.</param>
		/// <param name="condition">The condition to check.</param>
		/// <param name="messageResource">The resource string for the error message.</param>
		/// <param name="messageParams">The parameters to format the message.</param>
		/// <returns>The updated ValidationSet instance.</returns>
		public static ValidationSet AddValidation<T>(this ValidationSet validationSet, T obj, Func<T, bool> condition, string messageResource, params object[] messageParams)
		{
			var message = string.Format(messageResource, messageParams);
			return validationSet.AddValidation(obj, condition, message);
		}

		/// <summary>
		/// Checks if a condition is met and adds an error message if it is not.
		/// </summary>
		/// <typeparam name="T">The type of the object being validated.</typeparam>
		/// <param name="validationSet">The ValidationSet instance to add the validation to.</param>
		/// <param name="obj">The object to validate.</param>
		/// <param name="condition">The condition to check.</param>
		/// <param name="messageResource">The resource string for the error message.</param>
		/// <returns>The updated ValidationSet instance.</returns>
		public static ValidationSet AddValidation<T>(this ValidationSet validationSet, T obj, Func<T, bool> condition, string messageResource)
		{
			return validationSet.AddValidation(obj, condition, out _, messageResource);
		}

		/// <summary>
		/// Checks if a condition is met and adds an error message if it is not.
		/// </summary>
		/// <typeparam name="T">The type of the object being validated.</typeparam>
		/// <param name="validationSet">The ValidationSet instance to add the validation to.</param>
		/// <param name="obj">The object to validate.</param>
		/// <param name="condition">The condition to check.</param>
		/// <param name="isTrue">Outputs whether the condition was met or not.</param>
		/// <param name="messageResource">The resource string for the error message.</param>
		/// <returns>The updated ValidationSet instance.</returns>
		public static ValidationSet AddValidation<T>(this ValidationSet validationSet, T obj, Func<T, bool> condition, out bool isTrue, string messageResource)
		{
			isTrue = condition.Invoke(obj);
			if (!isTrue)
			{
				validationSet.AddError(messageResource);
			}
			return validationSet;
		}

		/// <summary>
		/// Checks if a condition is met and adds an error message if it is not.
		/// </summary>
		/// <param name="validationSet">The ValidationSet instance to add the validation to.</param>
		/// <param name="condition">The condition to check.</param>
		/// <param name="messageResource">The resource string for the error message.</param>
		/// <param name="messageParams">The parameters to format the message.</param>
		/// <returns>The updated ValidationSet instance.</returns>
		public static ValidationSet AddValidation(this ValidationSet validationSet, bool condition, string messageResource, params object[] messageParams)
		{
			if (!condition)
			{
				var message = string.Format(messageResource, messageParams);
				validationSet.AddError(message);
			}
			return validationSet;
		}
	}
}
