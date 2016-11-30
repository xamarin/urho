using System;

namespace Urho.Shapes
{
	public class Cylinder : Shape
	{
		public Cylinder() : base() { }
		public Cylinder(IntPtr handle) : base(handle) { }

		protected override string ModelResource => "Models/Cylinder.mdl";
	}
}