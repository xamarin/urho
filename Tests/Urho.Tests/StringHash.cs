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

		[Test]
		public void EqualityTest()
		{
			var hash1 = new StringHash(123);
			var hash2 = new StringHash(123);
			var hash3 = new StringHash(124);
			var hash4 = new StringHash(0);
			var hash5 = new StringHash("Hello");

			Assert.IsTrue(hash1 == hash2);
			Assert.IsTrue(hash1 != hash3);
			Assert.IsTrue(hash1 != hash4);
			Assert.IsTrue(hash1 != hash5);

			Assert.IsTrue(hash1.Equals(hash2));
			Assert.IsTrue(!hash1.Equals(hash3));


			Assert.IsTrue(hash1.GetHashCode() == hash2.GetHashCode());
			Assert.IsTrue(hash1.GetHashCode() != hash3.GetHashCode());
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
