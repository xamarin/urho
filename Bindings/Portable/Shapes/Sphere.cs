using System;

namespace Urho.Shapes
{
	public class Sphere : Shape
	{
		[Preserve]
		public Sphere() : base() { }
		[Preserve]
		public Sphere(IntPtr handle) : base(handle) { }

		protected override string ModelResource => "Models/Sphere.mdl";
	}
}