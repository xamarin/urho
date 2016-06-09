using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Urho.Actions;

namespace Urho.Tests
{
	[TestFixture]
	public class ActionManagerTests
	{
		[Test]
		public async Task TestRunActionsAsyncWorks ()
		{
			var app = await Task.Run(() => SimpleApplication.RunAsync (1, 1));
			var node = new MockNode ();

			bool called = false, called2 = false, called3 = false;
			await node.RunActionsAsync (new CallFunc (() => called = true));
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
		}
	}
}

