using System;

namespace Urho.Shapes
{
	public class Cone : Shape
	{
		[Preserve]
		public Cone() : base() { }
		[Preserve]
		public Cone(IntPtr handle) : base(handle) { }

		protected override string ModelResource => "Models/Cone.mdl";
	}
}