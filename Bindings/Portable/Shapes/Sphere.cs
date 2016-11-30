using System;

namespace Urho.Shapes
{
	public class Sphere : Shape
	{
		public Sphere() : base() { }
		public Sphere(IntPtr handle) : base(handle) { }

		protected override string ModelResource => "Models/Sphere.mdl";
	}
}