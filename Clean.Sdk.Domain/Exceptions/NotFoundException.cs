﻿using System;

namespace Clean.Domain.Exceptions
{
	public class NotFoundException
		: ValidationException
	{
		public NotFoundException(string? message)
			: base(message)
		{
		}

		public NotFoundException(string? message, Exception? innerException)
			: base(message, innerException)
		{
		}
	}
}
