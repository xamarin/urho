using System;
using System.Collections.Generic;
using Urho;
using Urho.Actions;
using Urho.HoloLens;

namespace Playgrounds.HoloLens
{
	public class SpatialMappingTests : StereoApplication
	{
		bool wireframe;
		bool mappingEnded;
		Vector3 envPositionBeforeManipulations;
		Node environmentNode;
		DebugHud debugHud;
		Material spatMaterial;

		public SpatialMappingTests(ApplicationOptions opts) : base(opts) { }

		protected override async void Start()
		{
			base.Start();

			new MonoDebugHud(this){ FpsOnly = true }.Show(Color.Green, 50);

			debugHud = Engine.CreateDebugHud();
			debugHud.ToggleAll();

			environmentNode = Scene.CreateChild();
			EnableGestureTapped = true;

			spatMaterial = new Material();
			spatMaterial.SetTechnique(0, CoreAssets.Techniques.NoTexture, 1, 1);
			spatMaterial.SetShaderParameter("MatDiffColor", Color.Cyan);

			// make sure 'spatialMapping' capabilaty is enabled in the app manifest.
			var cortanaAllowed = await RegisterCortanaCommands(new Dictionary<string, Action> {
					{ "show results" , ShowResults }
				});
			var spatialMappingAllowed = await StartSpatialMapping(new Vector3(50, 50, 50), color: Color.Yellow);
		}


		async void ShowResults()
		{
			EnableGestureManipulation = true;
			mappingEnded = true;
			StopSpatialMapping();

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

			if (isNew)
			{
				staticModel.SetMaterial(spatMaterial);
			}
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
