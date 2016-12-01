using System;

namespace Urho.Shapes
{
	public class Plane : Shape
	{
		[Preserve]
		public Plane() : base() { }
		[Preserve]
		public Plane(IntPtr handle) : base(handle) { }

		protected override string ModelResource => "Models/Plane.mdl";
	}
}
