namespace Clean.Sdk.Domain.Options
{
	/// <summary>
	/// Represents CORS configuration options.
	/// </summary>
	public class CorsOptions
	{
		/// <summary>
		/// Gets the name of the CORS policy.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the allowed origin for the CORS policy.
		/// </summary>
		public string Origin { get; private set; }

		/// <summary>
		/// Gets the allowed HTTP methods for the CORS policy.
		/// </summary>
		public string[] Methods { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CorsOptions"/> class.
		/// </summary>
		public CorsOptions()
		{
			Name = string.Empty;
			Origin = string.Empty;
			Methods = Array.Empty<string>();
		}
	}
}
