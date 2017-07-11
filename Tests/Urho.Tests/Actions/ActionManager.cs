using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Urho.Actions;
using Urho.Tests.Bootstrap;

namespace Urho.Tests
{
	[TestFixture]
	public class ActionManagerTests
	{
		[Test]
		public async Task TestRunActionsAsyncWorks()
		{
			TestApp.RunSimpleApp(async app =>
				{
					var node = new Node();

					bool called = false, called2 = false, called3 = false;
					await node.RunActionsAsync(new CallFunc(() => called = true));
					Assert.That(called, Is.True);

					called = false;
					called2 = false;
					called3 = false;

					await node.RunActionsAsync(
						new CallFunc(() => called = true),
						new CallFunc(() => called2 = true),
						new CallFunc(() => called3 = true)
					);

					Assert.That(called, Is.True);
					Assert.That(called2, Is.True);
					Assert.That(called3, Is.True);

					await app.Exit();
				});
		}

		[Test]
		public async Task TestIssue129()
		{
			TestApp.RunSimpleApp(async app =>
				{
					var scene = new Scene();
					var node = scene.CreateChild();
					await node.RunActionsAsync(new EaseIn(new MoveBy(1f, new Vector3(-10, -2, -10)), 1));
					node.Remove();
					await scene.CreateChild().RunActionsAsync(new EaseOut(new MoveBy(0.5f, new Vector3(0, 3, 0)), 1));
					await app.Exit();
				});
		}
	}
}