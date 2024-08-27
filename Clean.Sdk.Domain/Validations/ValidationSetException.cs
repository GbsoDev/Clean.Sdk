using System;

namespace Clean.Sdk.Domain.Validations
{
	public class ValidationSetException
		: Exception
	{
		private readonly ValidationSet _validationSet;

		public ValidationError[] Errors => _validationSet.Errors.ToArray();

		public ValidationSetException(ValidationSet validation, string? message)
			: base(message)
		{
			_validationSet = validation;
		}

		public ValidationSetException(ValidationSet validation)
			: this(validation, validation.ErrorMessage)
		{
			_validationSet = validation;
		}
	}
}
