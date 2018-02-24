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
using Urho.Shapes;

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
			arCore.ConfigRequested += this.ArCore_ConfigRequested;
			arCore.Camera = Camera;

			var boxNode = Scene.CreateChild();
			boxNode.Position = new Vector3(0, 0, 0.5f);
			boxNode.CreateComponent<Pyramid>();
			boxNode.SetScale(0.1f);

			new MonoDebugHud(this).Show(Color.Green, 16);
		}

		void ArCore_ConfigRequested(Config config)
		{
			config.SetPlaneFindingMode(Config.PlaneFindingMode.Horizontal);
		}

		void OnARFrameUpdated(Frame frame)
		{
		}
	}
}