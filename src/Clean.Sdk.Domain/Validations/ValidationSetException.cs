namespace Clean.Sdk.Domain.Validations
{
	/// <summary>
	/// Exception thrown when a <see cref="ValidationSet"/> contains validation errors.
	/// </summary>
	public class ValidationSetException
		: Exception
	{
		private readonly ValidationSet _validationSet;

		/// <summary>
		/// Gets the array of validation errors associated with this exception.
		/// </summary>
		public ValidationError[] Errors => _validationSet.Errors.ToArray();

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationSetException"/> class with a validation set and a custom message.
		/// </summary>
		/// <param name="validation">The validation set containing the errors.</param>
		/// <param name="message">The exception message.</param>
		public ValidationSetException(ValidationSet validation, string? message)
			: base(message)
		{
			_validationSet = validation;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationSetException"/> class with a validation set.
		/// </summary>
		/// <param name="validation">The validation set containing the errors.</param>
		public ValidationSetException(ValidationSet validation)
			: this(validation, validation.ErrorMessage)
		{
			_validationSet = validation;
		}
	}
}
