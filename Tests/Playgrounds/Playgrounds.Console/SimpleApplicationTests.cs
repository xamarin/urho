using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Urho;
using Urho.Shapes;

namespace Playgrounds.Console
{
	public class SimpleApplicationTests : SimpleApplication
	{
		public SimpleApplicationTests(ApplicationOptions options) : base(options) { }

		public static void RunApp()
		{
			var app = new SimpleApplicationTests(new ApplicationOptions());
			app.Run();
		}

		protected override void Start()
		{
			base.Start();

			var child = RootNode.CreateChild();
			child.Rotation = new Quaternion(0, 45, 0);
			var box = child.CreateComponent<Box>();

			RootNode.CreateComponent<WirePlane>();
		}
	}
}
