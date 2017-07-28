using NUnit.Framework;

namespace Urho.Tests
{
	[TestFixture]
	public class StringHashTests
	{
		[Test]
		public void HashAlgorithm()
		{
			string alphabet = "QWERTYUIOPASDFGHJKLZXCVBNM_qwertyuiopasdfghjklzxcvbnm";
			string value = "";
			for (int i = 0; i < 10000; i++)
			{
				int codeNative = new StringHash(value).Code;
				int codeManaged = GetHashManaged(value);
				Assert.AreEqual(codeManaged, codeNative);
				value += alphabet[i % alphabet.Length];
			}
		}

		// let's just check the hash algorithm was not changed.

		static int GetHashManaged(string str)
		{
			int hash = 0;
			if (string.IsNullOrEmpty(str))
				return 0;

			for (int i = 0; i < str.Length; i++)
				hash = char.ToLowerInvariant(str[i]) + (hash << 6) + (hash << 16) - hash;
			return hash;
		}
	}
}
