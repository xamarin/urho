using System;
using Urho.Resources;

namespace Urho.Shapes
{
	public abstract class Shape : StaticModel
	{
		Material material;

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

		Color color = Color.Magenta;
		public Color Color
		{
			set
			{
				if (material == null)
				{
					//try to restore material (after deserialization)
					material = GetMaterial(0);
					if (material == null)
					{
						material = new Material();
						material.SetTechnique(0, Application.ResourceCache.GetTechnique("Techniques/NoTextureAlpha.xml"), 1, 1);
					}
					SetMaterial(material);
				}
				material.SetShaderParameter("MatDiffColor", value);
				color = value;
			}
			get
			{
				return color;
			}
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
