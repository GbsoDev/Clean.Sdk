namespace Clean.Domain.Options
{
	public class CorsOptions
	{
		public string Name { get; set; }
		public string Origin { get; set; }
		public string[] Methods { get; set; }

		public CorsOptions()
		{
			Name = string.Empty;
			Origin = string.Empty;
			Methods = new string[0];
		}
	}
}
