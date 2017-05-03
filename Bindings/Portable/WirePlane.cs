using System;
using System.Collections.Generic;
using System.Text;

namespace Urho
{
	public class WirePlane : Component
	{
		CustomGeometry geom;
		private int size = 25;
		private float scale = 0.5f;

		public int Size
		{
			get { return size; }
			set
			{
				size = value;
				Reload();
			}
		}

		public float Scale
		{
			get { return scale; }
			set
			{
				scale = value;
				Reload();
			}
		}

		public override void OnAttachedToNode(Node node)
		{
			base.OnAttachedToNode(node);
			Reload();
		}

		void Reload()
		{
			if (geom != null && !geom.IsDeleted)
				geom.Remove();

			if (Node == null || Node.IsDeleted)
				return;

			Color color = new Color(0.8f, 0.8f, 0.8f);
			geom = Node.CreateComponent<CustomGeometry>();
			geom.BeginGeometry(0, PrimitiveType.LineList);
			var material = new Material();
			material.SetTechnique(0, CoreAssets.Techniques.NoTextureUnlitVCol, 1, 1);
			geom.SetMaterial(material);

			var halfSize = Size / 2;
			for (int i = -halfSize; i <= halfSize; i++)
			{
				//x
				geom.DefineVertex(new Vector3(i, 0, -halfSize) * Scale);
				geom.DefineColor(color);
				geom.DefineVertex(new Vector3(i, 0, halfSize) * Scale);
				geom.DefineColor(color);

				//z
				geom.DefineVertex(new Vector3(-halfSize, 0, i) * Scale);
				geom.DefineColor(color);
				geom.DefineVertex(new Vector3(halfSize, 0, i) * Scale);
				geom.DefineColor(color);
			}

			geom.Commit();
		}
	}
}
