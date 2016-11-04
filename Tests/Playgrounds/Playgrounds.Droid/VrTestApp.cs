using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Urho;
using Urho.Shapes;

namespace Playgrounds.Droid
{
	public class VrTestApp : StereoApplication
	{
		public VrTestApp(ApplicationOptions options) : base(options)
		{
		}

		protected override void Start()
		{
			base.Start();

			var rootNode = Scene.CreateChild();
			rootNode.Position = new Vector3(0, 0, 0.5f);
			rootNode.SetScale(0.15f);
			var box = rootNode.CreateComponent<Box>();
		}
	}
}