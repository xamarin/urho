using System;
using Urho.Actions;

namespace Urho.SharpReality
{
	public class SpatialCursor : Component
	{
		
		public SpatialCursor(IntPtr handle) : base(handle)
		{
			ReceiveSceneUpdates = true;
		}
		public SpatialCursor()
		{
			ReceiveSceneUpdates = true;
		}

		public Node CursorNode { get; private set; }
		public Node CursorModelNode { get; private set; }
		public bool CursorEnabled { get; set; } = true;

		public event Action<RayQueryResult?> Raycasted;

		public override void OnAttachedToNode(Node node)
		{
			CursorNode = node.CreateChild("SpatialCursor");
			CursorModelNode = CursorNode.CreateChild("SpatialCursorModel");
			CursorModelNode.SetScale(0.05f);
			var staticModel = CursorModelNode.CreateComponent<StaticModel>();
			staticModel.Model = CoreAssets.Models.Torus;
			Material mat = new Material();
			mat.SetTechnique(0, CoreAssets.Techniques.NoTextureOverlay, 1, 1);
			mat.SetShaderParameter("MatDiffColor", Color.Cyan);
			RunIdleAnimation();
			staticModel.SetMaterial(mat);
			staticModel.ViewMask = 0x80000000; //hide from raycasts
			base.OnAttachedToNode(node);
			ReceiveSceneUpdates = true;
		}

		public void RunIdleAnimation()
		{
			CursorModelNode.RemoveAllActions();
			CursorModelNode.RunActions(new RepeatForever(new ScaleTo(0.3f, 0.05f), new ScaleTo(0.3f, 0.03f)));
		}

		public async void ClickAnimation()
		{
			Color originalColor = Color.Cyan;
			Color clickColor = Color.Yellow;
			CursorModelNode.RemoveAllActions();

			var staticModel = CursorModelNode.GetComponent<StaticModel>();
			if (staticModel != null)
			{
				var specColorAnimation = new ValueAnimation();
				specColorAnimation.SetKeyFrame(0.0f, originalColor);
				specColorAnimation.SetKeyFrame(0.2f, clickColor);
				specColorAnimation.SetKeyFrame(0.4f, originalColor);
				var mat = staticModel.GetMaterial(0);
				mat?.SetShaderParameterAnimation("MatDiffColor", specColorAnimation, WrapMode.Once, 1.0f);
			}
			await CursorModelNode.RunActionsAsync(new ScaleTo(0.2f, 0.07f), new ScaleTo(0.4f, 0.04f));
			RunIdleAnimation();
		}

		Camera camera;

		private Camera Camera
		{
			get
			{
				if (camera == null)
				{
					var cc = this.Scene.GetComponent(Camera.TypeStatic, true);
					camera = this.Scene.GetComponent<Camera>(true);
					if (camera == null)
						throw new InvalidOperationException("Scene doesn't have a camera");
				}
				return camera;
			}
		}

		Octree octree;
		Octree Octree
		{
			get
			{
				if (octree == null)
				{
					octree = this.Scene.GetComponent<Octree>(true);
					if (octree == null)
						throw new InvalidOperationException("Scene doesn't have an octree");
				}
				return octree;
			}
		}

		protected override void OnUpdate(float timeStep)
		{
			base.OnUpdate(timeStep);
			Ray cameraRay = Camera.GetScreenRay(0.5f, 0.5f);
			var result = Octree.RaycastSingle(cameraRay, RayQueryLevel.Triangle, 100, DrawableFlags.Geometry, 0x70000000);
			Raycasted?.Invoke(result);
			if (!CursorEnabled)
				return;

			if (result != null)
			{
				CursorNode.Position = result.Value.Position;
				CursorNode.Rotation = FromLookRotation(new Vector3(0, 1, 0), result.Value.Normal);
			}
			else
				CursorNode.Position = Camera.Node.Rotation * new Vector3(0, 0, 5f);
		}

		static Quaternion FromLookRotation(Vector3 direction, Vector3 upDirection)
		{
			Vector3 v = Vector3.Cross(direction, upDirection);
			if (v.LengthSquared >= 0.1f)
			{
				v.Normalize();
				Vector3 y = Vector3.Cross(v, direction);
				Vector3 x = Vector3.Cross(y, direction);
				Matrix3 m3 = new Matrix3(
					x.X, y.X, direction.X,
					x.Y, y.Y, direction.Y,
					x.Z, y.Z, direction.Z);
				return new Quaternion(ref m3);
			}
			return Quaternion.Identity;
		}
	}
}
