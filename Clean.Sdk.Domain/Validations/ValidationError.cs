namespace Clean.Sdk.Domain.Validations
{
	public class ValidationError
	{
		public string Message { get; }

		public ValidationError(string message)
		{
			Message = message;
		}
	}
}
