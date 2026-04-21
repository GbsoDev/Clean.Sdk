using Clean.Sdk.Domain.Exceptions;
using Clean.Sdk.Domain.Resources;
using System.Security.Cryptography;
using System.Text;

namespace Clean.Sdk.Domain.Helpers
{
	/// <summary>
	/// Helper class for hashing operations.
	/// </summary>
	public static class HasherHelper
	{
		/// <summary>
		/// Computes the SHA256 hash of the specified input string.
		/// </summary>
		/// <param name="input">The string to hash.</param>
		/// <returns>The SHA256 hash as a hexadecimal string.</returns>
		/// <exception cref="InvalidArgumentException">Thrown when <paramref name="input"/> is null or white space.</exception>
		public static string ToSHA256(string input)
		{
			if (string.IsNullOrWhiteSpace(input)) throw new InvalidArgumentException(nameof(input), Messages.InputEmptyTextError);
			SHA256 sha256 = SHA256.Create();
			byte[] originalText = Encoding.Default.GetBytes(input);
			byte[] hash = sha256.ComputeHash(originalText);
			StringBuilder strBuilder = new StringBuilder();
			foreach (byte i in hash)
			{
				strBuilder.AppendFormat("{0:x2}", i);
			}
			return strBuilder.ToString();
		}
	}
}
