namespace Clean.Sdk.Domain.Options
{
	public class CorsOptions
	{
		public string Name { get; private set; }
		public string Origin { get; private set; }
		public string[] Methods { get; private set; }

		public CorsOptions()
		{
			Name = string.Empty;
			Origin = string.Empty;
			Methods = new string[0];
		}
	}
}
