using System;

namespace Urho.Shapes
{
	public class Plane : Shape
	{
		public Plane() : base() { }
		public Plane(IntPtr handle) : base(handle) { }

		protected override string ModelResource => "Models/Plane.mdl";
	}
}
