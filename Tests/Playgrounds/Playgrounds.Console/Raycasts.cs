using System;
using System.Collections.Generic;
using System.IO;
using Urho;
using Urho.Actions;
using Urho.Gui;
using Urho.Shapes;

namespace Playgrounds.Console
{
	public class Raycasts : SimpleApplication
	{
		public Raycasts(ApplicationOptions options) : base(options) { }

		public static void RunApp()
		{
			var app = new Raycasts(new ApplicationOptions(@"..\..\..\..\..\Urho3D\Source\bin\Data") { TouchEmulation = true });
			app.Run();
		}

		List<Node> points = new List<Node>();
		Node textNode;
		
		protected override void Start()
		{
			base.Start();

			this.Log.LogLevel = LogLevel.Warning;
			var planeNode = RootNode.CreateChild();
			planeNode.Scale = new Vector3(20, 1, 20);
			var plane = planeNode.CreateComponent<Urho.Shapes.Plane>();
			plane.Material = ResourceCache.GetMaterial("Materials/Stone.xml");

			var texture = plane.Material.GetTexture(TextureUnit.Diffuse);
			var glName = texture.AsGPUObject().GPUObjectName;

			pointerNode = Scene.CreateChild();
			pointerNode.SetScale(0.1f);
			var pointer = pointerNode.CreateComponent<Sphere>();
			pointer.Color = Color.Cyan;
			pointerNode.Name = "RulerPoint";

			textNode = pointerNode.CreateChild();
			textNode.SetScale(3);
			textNode.Translate(Vector3.UnitY * 2);
			textNode.AddRef();
			var text = textNode.CreateComponent<Text3D>();
			text.HorizontalAlignment = HorizontalAlignment.Center;
			text.VerticalAlignment = VerticalAlignment.Top;
			text.TextEffect = TextEffect.Stroke;
			text.EffectColor = Color.Black;
			text.SetColor(Color.White);
			text.SetFont(CoreAssets.Fonts.AnonymousPro, 50);

			Input.KeyUp += Input_KeyUp;
			
			var hud = new MonoDebugHud(this);
			hud.Show(Color.Red, 24);

			RenderPathCommand rpc = new RenderPathCommand(RenderCommandType.Quad);
			rpc.SetTextureName(TextureUnit.Diffuse, "Textures/UrhoDecal.dds");
			rpc.Type = RenderCommandType.Quad;
			rpc.VertexShaderName = (UrhoString)"CopyFramebuffer";
			rpc.PixelShaderName = (UrhoString)"CopyFramebuffer";
			rpc.SetOutput(0, "viewport");
			Viewport.RenderPath.InsertCommand(1, rpc);

		}

		private void Input_KeyUp(KeyUpEventArgs e)
		{
			if (cursorPos == null)
				return;

			var savedPoint = pointerNode;
			textNode.Parent.RemoveChild(textNode);

			points.Add(savedPoint);
			pointerNode = pointerNode.Clone();

			savedPoint.AddChild(textNode);

			if (points.Count > 1)
			{
				float distance = 0f;
				for (int i = 1; i < points.Count; i++)
					distance += Vector3.Distance(points[i - 1].Position, points[i].Position);
				textNode.GetComponent<Text3D>().Text = distance.ToString("F2") + "m";
			}

			if (prevNode != null)
				AddConnection(savedPoint, prevNode);
			prevNode = savedPoint;

			cursorPos = null;
		}

		Node prevNode;
		Node pointerNode;
		Vector3? cursorPos;

		protected override void OnUpdate(float timeStep)
		{
			base.OnUpdate(timeStep);
			var ray = Camera.GetScreenRay(0.5f, 0.5f);
			var raycastResult = Octree.RaycastSingle(ray);
			if (raycastResult != null)
			{
				var pos = raycastResult.Value.Position;
				if (!raycastResult.Value.Node.Name.StartsWith("RulerPoint"))
				{
					pointerNode.Position = pos;
					cursorPos = pos;
				}
			}
			else
			{
				cursorPos = null;
			}

			textNode.LookAt(CameraNode.Position, Vector3.UnitY);
			textNode.Rotate(new Quaternion(0, 180, 0));
		}

		private Material connectionMat;

		void AddConnection(Node point1, Node point2 = null)
		{
			const float size = 0.03f;
			var node = Scene.CreateChild();
			Vector3 v1 = point1.Position;
			Vector3 v2 = point2?.Position ?? Vector3.Zero;
			var distance = Vector3.Distance(v2, v1);
			node.Scale = new Vector3(size, Math.Abs(distance), size);
			node.Position = (v1 + v2) / 2f;
			node.Rotation = Quaternion.FromRotationTo(Vector3.UnitY, v1 - v2);
			var cylinder = node.CreateComponent<StaticModel>();
			cylinder.Model = CoreAssets.Models.LinePrimitives.UnitZ;
			cylinder.CastShadows = false;
			cylinder.SetMaterial(Material.FromColor(Color.White, true));
			node.RunActions(new TintTo(1f, 0f, 1f, 1f, 1f));
		}
	}
}
