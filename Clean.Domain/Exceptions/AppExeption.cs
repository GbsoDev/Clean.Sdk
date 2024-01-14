using System;

namespace Clean.Domain.Exceptions
{
	public class AppExeption : ApplicationException
	{

		public AppExeption(string message)
			: base(message)
		{
		}

		public AppExeption(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
