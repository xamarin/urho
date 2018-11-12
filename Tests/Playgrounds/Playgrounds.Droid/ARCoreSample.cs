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
using static Com.Google.AR.Core.Point;

namespace Playgrounds.Droid
{
	public class ARCoreSample : SimpleApplication
	{
		ARCoreComponent arCore;
		MonoDebugHud hud;

		public ARCoreSample(ApplicationOptions options) : base(options)
		{
		}

		protected override async void Start()
		{
			base.Start();

			arCore = Scene.CreateComponent<ARCoreComponent>();

			var boxNode = Scene.CreateChild();
			boxNode.Position = new Vector3(0, 0, 0.5f);
			boxNode.CreateComponent<Pyramid>();
			boxNode.SetScale(0.1f);

			hud = new MonoDebugHud(this);
			hud.Show(Color.Green, 40);

			Input.TouchEnd += Input_TouchEnd;

			await arCore.Run(Camera);
		}

		float touchX, touchY;

		void Input_TouchEnd(TouchEndEventArgs e)
		{
			touchX = e.X;
			touchY = e.Y;
		}

		void ArCore_ConfigRequested(Config config)
		{
			config.SetPlaneFindingMode(Config.PlaneFindingMode.Horizontal);
		}

		void OnARFrameUpdated(Frame frame)
		{
			if (touchX != 0)
			{
				var hitResult = frame.HitTest(touchX, touchY);
				foreach (var hit in hitResult.Take(1))
				{
					var track = hit.Trackable;
					if ((track is Com.Google.AR.Core.Plane plane && plane.IsPoseInPolygon(hit.HitPose)) ||
						(track is Com.Google.AR.Core.Point point && point.GetOrientationMode() == OrientationMode.EstimatedSurfaceNormal))
					{
						var anchor = hit.CreateAnchor();

						var testNode = Scene.CreateChild();
						testNode.CreateComponent<Urho.Shapes.Cylinder>().Color = Color.Green;
						var pose = hit.HitPose;
						testNode.Scale = new Vector3(0.1f, 0.2f, 0.1f);

						ARCoreComponent.TransformByPose(testNode, pose);

					}
				}
				touchX = touchY = 0;
			}
		}
	}
}