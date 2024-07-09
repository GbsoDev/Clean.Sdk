using System;

namespace Clean.Sdk.Domain.Options
{
	[Option(nameof(AuthOptions))]
	public class AuthOptions
	{
		public string Issuer { get; private set; }
		public string Audience { get; private set; }
		public string SigningKey { get; private set; }
		public string[] Roles { get; private set; }
		public TimeSpan ExpireTimeSpan { get; private set; }

		public AuthOptions()
		{
			Issuer = string.Empty;
			Audience = string.Empty;
			SigningKey = string.Empty;
			Roles = new string[0];
		}
	}
}
