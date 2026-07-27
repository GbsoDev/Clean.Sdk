namespace Clean.Sdk.Domain.Validations
{
	/// <summary>
	/// Represents a collection of validation errors and provides methods to manage them.
	/// </summary>
	public class ValidationSet
	{
		/// <summary>
		/// Gets the main error message for the validation set.
		/// </summary>
		public string? ErrorMessage { get; private set; }

		/// <summary>
		/// Gets a value indicating whether the validation set is valid (contains no errors).
		/// </summary>
		public bool IsValid => !Errors.Any();

		/// <summary>
		/// Gets the list of validation errors.
		/// </summary>
		public List<ValidationError> Errors { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationSet"/> class with a specified main error message.
		/// </summary>
		/// <param name="errorMessage">The main error message.</param>
		public ValidationSet(string? errorMessage)
			: this()
		{
			ErrorMessage = errorMessage;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationSet"/> class.
		/// </summary>
		public ValidationSet()
		{
			Errors = new List<ValidationError>();
		}

		/// <summary>
		/// Adds a new validation error to the set.
		/// </summary>
		/// <param name="message">The error message to add.</param>
		public void AddError(string message)
		{
			Errors.Add(new ValidationError(message));
		}

		/// <summary>
		/// Validates the set and throws a <see cref="ValidationSetException"/> if it is not valid.
		/// </summary>
		/// <exception cref="ValidationSetException">Thrown when the validation set contains one or more errors.</exception>
		public void ValidateAndThrow()
		{
			if (!IsValid)
			{
				throw new ValidationSetException(this, ErrorMessage);
			}
		}

		/// <summary>
		/// Sets the main error message for the validation set.
		/// </summary>
		/// <param name="errorMessage">The error message to set.</param>
		/// <returns>The current <see cref="ValidationSet"/> instance.</returns>
		public ValidationSet SetErrorMessage(string errorMessage)
		{
			this.ErrorMessage = errorMessage;
			return this;
		}
	}
}
