namespace Urho.Shapes
{
	public abstract class Shape : StaticModel
	{
		Material material;

		public override void OnAttachedToNode(Node node)
		{
			Model = Application.ResourceCache.GetModel(ModelResource);
		}

		protected virtual string ModelResource { get; }

		Color color;
		public Color Color
		{
			set
			{
				if (material == null)
				{
					material = new Material();
					material.SetTechnique(0, Application.ResourceCache.GetTechnique("Techniques/NoTextureAlpha.xml"), 1, 1);
					SetMaterial(material);
				}
				material.SetShaderParameter("MatDiffColor", value);
				color = value;
			}
			get { return color; }
		}
	}
}
