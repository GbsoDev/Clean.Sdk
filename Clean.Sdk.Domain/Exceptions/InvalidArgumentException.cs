namespace Clean.Sdk.Domain.Exceptions
{
	/// <summary>
	/// Exception thrown when an argument provided to a method is not valid.
	/// </summary>
	internal class InvalidArgumentException
		: AppExeption
	{
		/// <summary>
		/// Gets the name of the parameter that caused the current exception.
		/// </summary>
		public string ParamName { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidArgumentException"/> class.
		/// </summary>
		/// <param name="paramName">The name of the parameter that caused the exception.</param>
		/// <param name="message">The message that describes the error.</param>
		public InvalidArgumentException(string paramName, string message) : base(message)
		{
			ParamName = paramName;
		}

	}
}
