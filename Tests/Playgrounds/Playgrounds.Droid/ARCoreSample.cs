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
using Com.Google.AR.Core;
using Urho;
using Urho.Droid;

namespace Playgrounds.Droid
{
	public class ARCoreSample : SimpleApplication
	{
		ARCoreComponent arCore;

		public ARCoreSample(ApplicationOptions options) : base(options)
		{
		}

		protected override void Start()
		{
			base.Start();

			arCore = Scene.CreateComponent<ARCoreComponent>();
			arCore.ARFrameUpdated += OnARFrameUpdated;
			arCore.Camera = Camera;
		}

		private void OnARFrameUpdated(Frame frame)
		{
		}
	}
}