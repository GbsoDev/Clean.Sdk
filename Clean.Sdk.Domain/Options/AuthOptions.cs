namespace Clean.Sdk.Domain.Options
{
	/// <summary>
	/// Represents authentication configuration options.
	/// </summary>
	[Option(nameof(AuthOptions))]
	public class AuthOptions
	{
		/// <summary>
		/// Gets the issuer of the authentication token.
		/// </summary>
		public string Issuer { get; private set; }

		/// <summary>
		/// Gets the audience of the authentication token.
		/// </summary>
		public string Audience { get; private set; }

		/// <summary>
		/// Gets the key used for signing the authentication token.
		/// </summary>
		public string SigningKey { get; private set; }

		/// <summary>
		/// Gets the roles allowed in the application.
		/// </summary>
		public string[] Roles { get; private set; }

		/// <summary>
		/// Gets the expiration time span for the authentication token.
		/// </summary>
		public TimeSpan ExpireTimeSpan { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthOptions"/> class.
		/// </summary>
		public AuthOptions()
		{
			Issuer = string.Empty;
			Audience = string.Empty;
			SigningKey = string.Empty;
			Roles = Array.Empty<string>();
		}
	}
}
