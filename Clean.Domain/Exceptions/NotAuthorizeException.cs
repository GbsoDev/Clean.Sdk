using System;

namespace Clean.Domain.Exceptions
{
	public class NotAuthorizeException
		: ValidationException
	{
		public NotAuthorizeException(string? message) : base(message)
		{
		}

		public NotAuthorizeException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
	}
}
