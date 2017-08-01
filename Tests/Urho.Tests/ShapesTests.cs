using System.Threading.Tasks;
using NUnit.Framework;
using Urho.Shapes;
using Urho.Tests.Bootstrap;

namespace Urho.Tests
{
	[TestFixture]
	public class ShapesTests
	{
		[Test]
		public async Task Shapes_Clone()
		{
			TestApp.RunSimpleApp(async app =>
			{
				var node = app.RootNode.CreateChild();
				var box = node.CreateComponent<Box>();
				box.Color = Color.Cyan;

				var clone1 = node.Clone();
				var clonedBox1 = clone1.GetComponent<Box>();
				
				var clone2 = clone1.Clone();
				var clonedBox2 = clone2.GetComponent<Box>();

				var clone3 = clone2.Clone();
				var clonedBox3 = clone3.GetComponent<Box>();

				Assert.AreEqual(Color.Cyan, clonedBox1.Color);
				Assert.AreEqual(Color.Cyan, clonedBox2.Color);
				Assert.AreEqual(Color.Cyan, clonedBox3.Color);

				await app.Exit();
			});
		}
	}
}
