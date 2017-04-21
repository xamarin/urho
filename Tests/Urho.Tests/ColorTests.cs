using NUnit.Framework;

namespace Urho.Tests
{
	[TestFixture]
	public class ColorTests
	{
		[Test]
		public void FromHex()
		{
			Assert.AreEqual(new Color(1, 0, 0, 1), Color.FromHex("#FFFF0000"));
			Assert.AreEqual(new Color(1, 0, 0, 1), Color.FromHex("#FF0000"));
			Assert.AreEqual(new Color(1, 1, 1, 1), Color.FromHex("#FFFFFFFF"));
			Assert.AreEqual(new Color(1, 1, 1, 1), Color.FromHex("#FFFFFF"));
			Assert.AreEqual(new Color(0, 0, 0, 0), Color.FromHex("#00000000"));
			Assert.AreEqual(new Color(0, 1, 0, 1), Color.FromHex("#00FF00"));
			Assert.AreEqual(new Color(0, 1, 0, 1), Color.FromHex("#FF00FF00"));
			Assert.AreEqual(new Color(0, 0, 1, 1), Color.FromHex("#0000FF"));
			Assert.AreEqual(new Color(0, 0, 1, 1), Color.FromHex("#FF0000FF"));
		}

		[Test]
		public void Casts()
		{
			Vector3 vec3 = new Vector3(0.1f, 0.2f, 0.3f);
			Vector4 vec4 = new Vector4(0.1f, 0.2f, 0.3f, 0.4f);

			Color color3 = new Color(0.1f, 0.2f, 0.3f);
			Color color4 = new Color(0.1f, 0.2f, 0.3f, 0.4f);

			Assert.AreEqual(color3, (Color) vec3);
			Assert.AreEqual(color4, (Color) vec4);
		}
	}
}
