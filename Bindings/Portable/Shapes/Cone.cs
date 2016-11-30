using System;

namespace Urho.Shapes
{
	public class Cone : Shape
	{
		public Cone() : base() { }
		public Cone(IntPtr handle) : base(handle) { }

		protected override string ModelResource => "Models/Cone.mdl";
	}
}