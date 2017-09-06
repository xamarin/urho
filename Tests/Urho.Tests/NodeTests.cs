using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using Urho.Tests.Bootstrap;

namespace Urho.Tests
{
	[TestFixture]
	public class NodeTests
	{
		[Test]
		public async Task GetComponent()
		{
			TestApp.RunSimpleApp(async app =>
			{
				CreateHeirarchy(app.RootNode, 0);

				Assert.IsNotNull(app.Scene.GetComponent<Octree>(false));
				Assert.IsNotNull(app.Scene.GetComponent<Octree>(true));

				Assert.IsNull(app.Scene.GetComponent<ManagedComponent>(false));
				Assert.IsNotNull(app.Scene.GetComponent<ManagedComponent>(true));

				Stopwatch sw = Stopwatch.StartNew();
				for (int i = 0; i < 10; i++)
					Assert.IsNotNull(app.Scene.GetComponent<ManagedComponent>(true));
				sw.Stop();
				System.Console.WriteLine($"GetComponent<C#>: {sw.ElapsedMilliseconds}ms");

				sw.Restart();
				for (int i = 0; i < 5; i++)
					Assert.IsNotNull(app.Scene.GetComponent<AnimationController>(true));
				sw.Stop();
				System.Console.WriteLine($"GetComponent<C++>: {sw.ElapsedMilliseconds}ms");


				var node1 = app.Scene.CreateChild();
				var node2 = node1.CreateChild("node2");
				node2.AddTag("foo");
				var node3 = node1.CreateChild("node3");
				node3.AddTag("boo");
				node3.AddTag("foo");
				var node4 = node1.CreateChild("node3");
				node4.AddTag("boo");

				var fooNodes = node1.GetChildrenWithTag("foo", true);
				Assert.AreEqual(2, fooNodes.Length);
				Assert.AreEqual(fooNodes[0].Name, "node2");
				Assert.AreEqual(fooNodes[1].Name, "node3");

				fooNodes = node1.GetChildrenWithTag("foo", false);
				Assert.AreEqual(0, fooNodes.Length);

				await app.Exit();
			});
		}

		const int MaxLevel = 5;

		static void CreateHeirarchy(Node node, int level)
		{
			if (level > MaxLevel)
				return;

			node.CreateComponent<Camera>();
			node.CreateComponent<StaticModel>();
			node.CreateComponent<AnimatedModel>();
			node.CreateComponent<ManagedCamera>();

			if (level == MaxLevel)
			{
				node.CreateComponent<AnimationController>();
				node.CreateComponent<Component>();
				node.CreateComponent<ManagedComponent>();
			}

			for (int i = 0; i < MaxLevel; i++)
			{
				CreateHeirarchy(node.CreateChild($"Node_{level}_{i}"), level + 1);
			}
		}
	}

	public class ManagedCamera : Camera { }
	public class ManagedComponent : Component { }
}
