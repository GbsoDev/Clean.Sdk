namespace Clean.Sdk.Domain.Validations
{
	/// <summary>
	/// Represents a validation error with a specific message.
	/// </summary>
	public class ValidationError
	{
		/// <summary>
		/// Gets the error message.
		/// </summary>
		public string Message { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationError"/> class with a specified message.
		/// </summary>
		/// <param name="message">The validation error message.</param>
		public ValidationError(string message)
		{
			Message = message;
		}
	}
}
