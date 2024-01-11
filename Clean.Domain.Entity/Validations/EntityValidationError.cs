namespace Clean.Domain.Entity.Validations
{
	public class EntityValidationError
	{
		public string Message { get; }

		public EntityValidationError(string message)
		{
			Message = message;
		}
	}
}
