using System;

namespace Clean.Sdk.Domain.Exceptions
{
	public class NullOrEmptyException
		: ValidationException
	{
		public NullOrEmptyException(string? message)
			: base(message)
		{
		}

		public NullOrEmptyException(string? message, Exception? innerException)
			: base(message, innerException)
		{
		}
	}
}
