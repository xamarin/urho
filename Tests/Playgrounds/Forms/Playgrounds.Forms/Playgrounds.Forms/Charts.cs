using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Urho;
using Urho.Actions;
using Urho.Gui;
using Urho.Shapes;

namespace Playgrounds.Forms
{
	public class Charts : Application
	{
		bool movementsEnabled;
		Scene scene;
		Node plotNode;
		Camera camera;
		Octree octree;
		List<Bar> bars;

		public Bar SelectedBar { get; private set; }

		public IEnumerable<Bar> Bars => bars;

		[Preserve]
		public Charts(ApplicationOptions options = null) : base(options) { }

		protected override void Start ()
		{
			base.Start ();
			CreateScene ();
			SetupViewport ();
		}

		async void CreateScene ()
		{
			Input.SubscribeToTouchEnd(OnTouched);

			new MonoDebugHud(this).Show();
			var cache = ResourceCache;
			scene = new Scene ();
			octree = scene.CreateComponent<Octree> ();

			plotNode = scene.CreateChild();
			var baseNode = plotNode.CreateChild().CreateChild();
			var plane = baseNode.CreateComponent<StaticModel>();
			plane.Model = CoreAssets.Models.Plane;

			var cameraNode = scene.CreateChild ("camera");
			camera = cameraNode.CreateComponent<Camera>();
			cameraNode.Position = new Vector3(10, 15, 10) / 1.75f;
			cameraNode.Rotation = new Quaternion(-0.121f, 0.878f, -0.305f, -0.35f);

			Node lightNode = cameraNode.CreateChild(name: "light");
			var light = lightNode.CreateComponent<Light>();
			light.LightType = LightType.Point;
			light.Range = 100;
			light.Brightness = 1.3f;

			int size = 3;
			baseNode.Scale = new Vector3(size * 1.5f, 1, size * 1.5f);
			bars = new List<Bar>(size * size);
			for (var i = 0f; i < size * 1.5f; i += 1.5f)
			{
				for (var j = 0f; j < size * 1.5f; j += 1.5f)
				{
					var boxNode = plotNode.CreateChild();
					boxNode.Position = new Vector3(size / 2f - i, 0, size / 2f - j);
					var box = new Bar(new Color(RandomHelper.NextRandom(), RandomHelper.NextRandom(), RandomHelper.NextRandom(), 0.9f));
					boxNode.AddComponent(box);
					box.SetValueWithAnimation((Math.Abs(i) + Math.Abs(j) + 1) / 2f);
					bars.Add(box);
				}
			}
			SelectedBar = bars.First();
			SelectedBar.Select();
			await plotNode.RunActionsAsync(new EaseBackOut(new RotateBy(2f, 0, 360, 0)));
			movementsEnabled = true;
		}

		void OnTouched(TouchEndEventArgs e)
		{
			Ray cameraRay = camera.GetScreenRay((float)e.X / Graphics.Width, (float)e.Y / Graphics.Height);
			var results = octree.RaycastSingle(cameraRay, RayQueryLevel.Triangle, 100, DrawableFlags.Geometry);
			if (results != null)
			{
				var bar = results.Value.Node?.Parent?.GetComponent<Bar>();
				if (SelectedBar != bar)
				{
					SelectedBar?.Deselect();
					SelectedBar = bar;
					SelectedBar?.Select();
				}
			}
		}

		protected override void OnUpdate(float timeStep)
		{
			if (Input.NumTouches >= 1 && movementsEnabled)
			{
				var touch = Input.GetTouch(0);
				plotNode.Rotate(new Quaternion(0, -touch.Delta.X, 0), TransformSpace.Local);
			}
			base.OnUpdate(timeStep);
		}

		public void Rotate(float toValue)
		{
			plotNode.Rotate(new Quaternion(0, toValue, 0), TransformSpace.Local);
		}
		
		void SetupViewport ()
		{
			var renderer = Renderer;
			renderer.SetViewport (0, new Viewport (Context, scene, camera, null));
		}
	}

	public class Bar : Component
	{
		Node barNode;
		Node textNode;
		Text3D text3D;
		Color color;
		float lastUpdateValue;

		public float Value
		{
			get { return barNode.Scale.Y; }
			set { barNode.Scale = new Vector3(1, value < 0.3f ? 0.3f : value, 1); }
		}

		public void SetValueWithAnimation(float value) => barNode.RunActionsAsync(new EaseBackOut(new ScaleTo(3f, 1, value, 1)));

		public Bar(Color color)
		{
			this.color = color;
			ReceiveSceneUpdates = true;
		}

		public override void OnAttachedToNode(Node node)
		{
			barNode = node.CreateChild();
			barNode.Scale = new Vector3(1, 0, 1); //means zero height
			var box = barNode.CreateComponent<Box>();
			box.Color = color;

			textNode = node.CreateChild();
			textNode.Rotate(new Quaternion(0, 180, 0), TransformSpace.World);
			textNode.Position = new Vector3(0, 10, 0);
			text3D = textNode.CreateComponent<Text3D>();
			text3D.SetFont(CoreAssets.Fonts.AnonymousPro, 60);
			text3D.TextEffect = TextEffect.Stroke;
			//textNode.LookAt() //Look at camera

			base.OnAttachedToNode(node);
		}

		protected override void OnUpdate(float timeStep)
		{
			var pos = barNode.Position;
			var scale = barNode.Scale;
			barNode.Position = new Vector3(pos.X, scale.Y / 2f, pos.Z);
			textNode.Position = new Vector3(0.5f, scale.Y + 0.2f, 0);
			var newValue = (float)Math.Round(scale.Y, 1);
			if (lastUpdateValue != newValue)
				text3D.Text = newValue.ToString("F01", CultureInfo.InvariantCulture);
			lastUpdateValue = newValue;
		}

		public void Deselect()
		{
			barNode.RemoveAllActions();//TODO: remove only "selection" action
			barNode.RunActionsAsync(new EaseBackOut(new TintTo(1f, color.R, color.G, color.B)));
		}

		public void Select()
		{
			Selected?.Invoke(this);
			// "blinking" animation
			barNode.RunActionsAsync(new RepeatForever(new TintTo(0.3f, 1f, 1f, 1f), new TintTo(0.3f, color.R, color.G, color.B)));
		}

		public event Action<Bar> Selected;
	}
}