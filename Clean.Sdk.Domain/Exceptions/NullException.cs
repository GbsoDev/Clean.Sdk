using System;

namespace Clean.Sdk.Domain.Exceptions
{
	public class NullException
		: ValidationException
	{
		public NullException(string? message)
			: base(message)
		{
		}

		public NullException(string? message, Exception? innerException)
			: base(message, innerException)
		{
		}
	}
}
