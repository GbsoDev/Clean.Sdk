using System;

namespace Clean.Sdk.Domain.Validations
{
	public class ValidationException
		: Exception
	{
		public readonly ValidationSet _validationSet;

		public ValidationError[] Errors => _validationSet.Errors.ToArray();

		public ValidationException(ValidationSet validation, string? message)
			: base(message)
		{
			_validationSet = validation;
		}

		public ValidationException(ValidationSet validation)
			: this(validation, null)
		{
			_validationSet = validation;
		}
	}
}
