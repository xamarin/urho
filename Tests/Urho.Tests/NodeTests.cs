using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
