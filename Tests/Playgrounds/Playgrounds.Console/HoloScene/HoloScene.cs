using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Newtonsoft.Json;
using Urho;
using Urho.HoloLens;
namespace Playgrounds.Console.HoloScene
{
	public class HoloScene : HoloApplication
	{
		private Node environmentNode;

		public HoloScene(ApplicationOptions opts) : base(opts)
		{
		}

		public static void RunApp()
		{
			var app = new HoloScene(new ApplicationOptions()
				{
					Width = 1268,
					Height = 720,
				});
			app.Emulator = true;
			app.Run();
		}


		protected override unsafe void Start()
		{
			base.Start();
			environmentNode = Scene.CreateChild();

			environmentNode.SetScale(0.2f);
			var surfs = JsonConvert.DeserializeObject<Dictionary<string, SurfaceDto>>(File.ReadAllText(@"HoloLens\SpatialData.txt"));

			var randomMaterial = Material.FromColor(Color.Gray);
			randomMaterial.CullMode = CullMode.Ccw;
			randomMaterial.FillMode = FillMode.Solid;

			foreach (var item in surfs)
			{
				var surface = item.Value;
				var model = CreateModelFromVertexData(surface);
				if (model == null)
					continue;

				var orient = surface.BoundsOrientation;
				var bounds = surface.BoundsCenter;

				var child = environmentNode.CreateChild(item.Key);
				var staticModel = child.CreateComponent<StaticModel>();
				staticModel.Model = model;

				child.Position = *(Vector3*)(void*)&bounds;
				child.Rotation = *(Quaternion*)(void*)&orient;

				staticModel.SetMaterial(randomMaterial);
			}
		}

		unsafe Model CreateModelFromVertexData(SurfaceDto surface)
		{
			var model = new Model();
			var vertexBuffer = new VertexBuffer(Context, false);
			var indexBuffer = new IndexBuffer(Context, false);
			var geometry = new Geometry();

			vertexBuffer.Shadowed = true;
			vertexBuffer.SetSize((uint)surface.VertexData.Length, ElementMask.Position | ElementMask.Normal | ElementMask.Color, false);

			var orient = surface.BoundsOrientation;
			var bounds = surface.BoundsCenter;
			var urhoBounds  = *(Vector3*)(void*)&bounds;
			var urhoQuaternion = *(Quaternion*)(void*)&orient;

			var indecesList = surface.IndexData.ToList();
			
			for (int i = 0; i < surface.VertexData.Length; i++)
			{
				var data = surface.VertexData[i];
				var worldPos = urhoQuaternion * new Vector3(data.Position.X, data.Position.Y, data.Position.Z) + urhoBounds;
			}

			if (indecesList.Count < 1)
				return null;
			
			fixed (SpatialVertexDto* p = &surface.VertexData[0])
			{
				vertexBuffer.SetData((void*)p);
			}

			var indexData = indecesList.ToArray();// surface.IndexData;
			indexBuffer.Shadowed = true;
			indexBuffer.SetSize((uint)indexData.Length, false, false);
			indexBuffer.SetData(indexData);

			geometry.SetVertexBuffer(0, vertexBuffer);
			geometry.IndexBuffer = indexBuffer;
			geometry.SetDrawRange(PrimitiveType.TriangleList, 0, (uint)indexData.Length, 0, (uint)surface.VertexData.Length, true);

			model.NumGeometries = 1;
			model.SetGeometry(0, 0, geometry);
			model.BoundingBox = new BoundingBox(new Vector3(-1.26f, -1.26f, -1.26f), new Vector3(1.26f, 1.26f, 1.26f));

			return model;
		}
	}
}
