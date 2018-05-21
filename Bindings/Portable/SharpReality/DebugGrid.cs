using Urho.Shapes;

namespace Urho.SharpReality
{
	public class DebugGrid : Component
	{
		public override void OnAttachedToNode(Node node)
		{
			const int horizontalSize = 5;
			const float scale = 1f;
			Color color = new Color(0.7f, 0.7f, 0.7f);

			var geom = node.CreateComponent<CustomGeometry>();
			geom.BeginGeometry(0, PrimitiveType.LineList);
			var material = new Material();
			material.SetTechnique(0, CoreAssets.Techniques.NoTextureUnlitVCol, 1, 1);
			geom.SetMaterial(material);

			var halfSize = horizontalSize / 2;
			for (int i = -halfSize; i <= halfSize; i++)
			{
				for (int j = -halfSize; j <= halfSize; j++)
				{
					//x
					geom.DefineVertex(new Vector3(i, j, -halfSize) * scale);
					geom.DefineColor(color);
					geom.DefineVertex(new Vector3(i, j, halfSize) * scale);
					geom.DefineColor(color);

					//y
					geom.DefineVertex(new Vector3(j, -halfSize, i) * scale);
					geom.DefineColor(color);
					geom.DefineVertex(new Vector3(j, halfSize, i) * scale);
					geom.DefineColor(color);

					//z
					geom.DefineVertex(new Vector3(-halfSize, j, i) * scale);
					geom.DefineColor(color);
					geom.DefineVertex(new Vector3(halfSize, j, i) * scale);
					geom.DefineColor(color);
				}
			}

			var origin = node.CreateChild();
			origin.SetScale(0.05f);
			origin.CreateComponent<Urho.Shapes.Sphere>();

			geom.Commit();
			base.OnAttachedToNode(node);
		}
	}
}
