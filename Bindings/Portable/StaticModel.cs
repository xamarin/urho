namespace Urho
{
	partial class StaticModel
	{
		public Material Material
		{
			get { return GetMaterial(0); }
			set { SetMaterial(0, value); }
		}
	}

	partial class AnimatedModel
	{
		public Model Model
		{
			get { return base.Model; }
			set { this.SetModel(value, true); }
		}
	}
}