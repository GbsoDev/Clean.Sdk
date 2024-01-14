using System;

namespace Clean.Domain.Exceptions
{
	public class NullObjectException
		: ValidationException
	{
		public NullObjectException(string? message)
			: base(message)
		{
		}

		public NullObjectException(string? message, Exception? innerException)
			: base(message, innerException)
		{
		}
	}
}
