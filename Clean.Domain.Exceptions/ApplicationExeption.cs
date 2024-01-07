using System;

namespace Clean.Domain.Exceptions
{
	public class ApplicationExeption : Exception
	{

		public ApplicationExeption(string mensaje)
			: base(mensaje)
		{
		}

		public ApplicationExeption(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
