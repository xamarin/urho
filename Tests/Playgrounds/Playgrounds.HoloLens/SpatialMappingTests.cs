using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Actions;
using Urho.HoloLens;

namespace Playgrounds.HoloLens
{
	public class SpatialMappingTests : HoloApplication
	{
		bool wireframe;
		bool mappingEnded;
		Vector3 envPositionBeforeManipulations;
		Node environmentNode;
		DebugHud debugHud;
		Material spatMaterial;

		public SpatialMappingTests(string assets) : base(assets) { }

		protected override async void Start()
		{
			base.Start();

			new MonoDebugHud(this){ FpsOnly = true }.Show(Color.Green, 50);

			debugHud = Engine.CreateDebugHud();
			debugHud.ToggleAll();

			environmentNode = Scene.CreateChild();
			EnableGestureTapped = true;

			spatMaterial = new Material();
			spatMaterial.CullMode = CullMode.MaxCullmodes;
			spatMaterial.ShadowCullMode = CullMode.MaxCullmodes;
			spatMaterial.SetTechnique(0, CoreAssets.Techniques.NoTextureUnlitVCol, 1, 1);

			// make sure 'spatialMapping' capabilaty is enabled in the app manifest.
			var cortanaAllowed = await RegisterCortanaCommands(new Dictionary<string, Action> {
					{ "show results" , ShowResults }
				});
			var spatialMappingAllowed = await StartSpatialMapping(new Vector3(50, 50, 50), color: Color.Yellow);
		}


		async void ShowResults()
		{
			var text1 = debugHud.MemoryText.Value;
			var text2 = debugHud.ModeText.Value;
			var text3 = debugHud.ProfilerText.Value;
			var text4 = debugHud.StatsText.Value;

			EnableGestureManipulation = true;
			mappingEnded = true;
			StopSpatialMapping();

			var material = new Material();
			material.CullMode = CullMode.Ccw;
			material.SetTechnique(0, CoreAssets.Techniques.NoTextureUnlitVCol, 1, 1);

			foreach (var node in environmentNode.Children)
				node.GetComponent<StaticModel>().SetMaterial(material);

			var previewPosition = LeftCamera.Node.Position + (LeftCamera.Node.Direction * 0.5f);
			environmentNode.Position = previewPosition;

			await environmentNode.RunActionsAsync(new EaseOut(new ScaleTo(1f, 0.03f), 1f));
		}

		public override void OnGestureTapped()
		{
			if (mappingEnded)
				return;

			wireframe = !wireframe;
			foreach (var node in environmentNode.Children)
			{
				var material = node.GetComponent<StaticModel>().GetMaterial(0);
				material.FillMode = wireframe ? FillMode.Wireframe : FillMode.Solid;
			}
		}

		public override Model GenerateModelFromSpatialSurface(SpatialMeshInfo surface)
		{
			// modify VertexColor for the VCol technique
			for (int i = 0; i < surface.VertexData.Length; i++)
			{
				var vtx = surface.VertexData[i];
				var worldPosition = surface.BoundsRotation * vtx.Position + surface.BoundsCenter;
				surface.VertexData[i].Color = new Color((worldPosition.Y + 2f) / 3f, 0.5f, 0);
			}

			return CreateModelFromVertexData(surface.VertexData, surface.IndexData);
		}

		public override void OnSurfaceAddedOrUpdated(SpatialMeshInfo surface, Model generatedModel)
		{
			if (mappingEnded)
				return;

			bool isNew = false;
			StaticModel staticModel = null;
			Node node = environmentNode.GetChild(surface.SurfaceId, false);
			if (node != null)
			{
				isNew = false;
				staticModel = node.GetComponent<StaticModel>();
			}
			else
			{
				isNew = true;
				node = environmentNode.CreateChild(surface.SurfaceId);
				staticModel = node.CreateComponent<StaticModel>();
			}

			node.Position = surface.BoundsCenter;
			node.Rotation = surface.BoundsRotation;
			staticModel.Model = generatedModel;

			Material mat;
			Color startColor;
			Color endColor = new Color(0.8f, 0.8f, 0.8f);

			if (isNew)
			{
				//startColor = Color.Blue;
				//mat = Material.FromColor(endColor);
				staticModel.SetMaterial(spatMaterial);
			}
			else
			{
				startColor = Color.Red;
				mat = staticModel.GetMaterial(0);
			}

			//mat.FillMode = wireframe ? FillMode.Wireframe : FillMode.Solid;
			//var specColorAnimation = new ValueAnimation();
			//specColorAnimation.SetKeyFrame(0.0f, startColor);
			//specColorAnimation.SetKeyFrame(1.5f, endColor);
			//mat.SetShaderParameterAnimation("MatDiffColor", specColorAnimation, WrapMode.Once, 1.0f);
		}

		public override void OnGestureManipulationStarted()
		{
			envPositionBeforeManipulations = environmentNode.Position;
		}

		public override void OnGestureManipulationUpdated(Vector3 relativeHandPosition)
		{
			environmentNode.Position = relativeHandPosition + envPositionBeforeManipulations;
		}
	}
}
