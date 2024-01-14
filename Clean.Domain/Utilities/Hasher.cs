using Clean.Domain.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace Clean.Domain.Utilities
{
	public static class Hasher
	{
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
