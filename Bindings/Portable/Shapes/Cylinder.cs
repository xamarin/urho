using System;

namespace Urho.Shapes
{
	public class Cylinder : Shape
	{
		[Preserve]
		public Cylinder() : base() { }
		[Preserve]
		public Cylinder(IntPtr handle) : base(handle) { }

		protected override string ModelResource => "Models/Cylinder.mdl";
	}
}