namespace Clean.Sdk.Domain.Exceptions
{
	internal class InvalidArgumentException
		: AppExeption
	{
		public string ParamName { get; }

		public InvalidArgumentException(string paramName, string message) : base(message)
		{
			ParamName = paramName;
		}

	}
}
