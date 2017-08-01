using System;
using Urho.Resources;

namespace Urho.Shapes
{
	public abstract class Shape : StaticModel
	{
		Material material;
		Color color = Color.Magenta;
		bool alphaTechnique;

		[Preserve]
		protected Shape() : base() { }
		[Preserve]
		protected Shape(IntPtr handle) : base(handle) { }

		public override void OnAttachedToNode(Node node)
		{
			Model = Application.ResourceCache.GetModel(ModelResource);
			Color = color;
			CastShadows = true;
		}

		protected abstract string ModelResource { get; }

		public Color Color
		{
			set
			{
				const float tolerance = 0.001f;
				if (
					// we had NoTextureAlpha but now user requests color with A=1 -> change to NoTexture
					(alphaTechnique && Math.Abs(value.A - 1) < tolerance) ||
					// we had NoTexture but now user requests color with A<1 -> change to NoTextureAlpha
					(!alphaTechnique && value.A < (1 - tolerance)) ||
					// material is not set yet
					material == null)
				{
					alphaTechnique = value.A < (1 - tolerance); 
					material = Material.FromColor(value);
					SetMaterial(material);
					color = value;
					return;
				}
				material.SetShaderParameter("MatDiffColor", value);
				color = value;
			}
			get { return color; }
		}

		public override void OnDeserialize(IComponentDeserializer d)
		{
			color = d.Deserialize<Color>(nameof(Color));
		}

		public override void OnSerialize(IComponentSerializer s)
		{
			s.Serialize(nameof(Color), Color);
		}

		public override void OnCloned(Scene scene, Component originalComponent)
		{
			var shape = (Shape)originalComponent;
			Color = shape.Color;
		}
	}
}
